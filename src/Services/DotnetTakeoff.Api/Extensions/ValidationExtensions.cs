using System.Reflection;
using FluentValidation;

namespace DotnetTakeoff.Api.Extensions;

internal static class ValidationExtensions
{
    private static readonly IDictionary<Type, Type> ValidatedTypes = new Dictionary<Type, Type>();

    public static WebApplicationBuilder AddValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<Program>(includeInternalTypes: true, filter: result =>
        {
            var validatedType = result.InterfaceType.GetGenericArguments()[0];
            var validatorType = result.ValidatorType;
            ValidatedTypes.Add(validatedType, validatorType);

            return true;
        });

        return builder;
    }

    public static TBuilder WithValidation<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpoint =>
        {
            var parametersValidatorTypes = GetParametersValidatorTypes(endpoint);
            if (parametersValidatorTypes.Count == 0)
            {
                // Nothing to validate, don't add the filter to the endpoint
                return;
            }

            endpoint.FilterFactories.Add((_, next) =>
            {
                return async invocationContext =>
                {
                    foreach (var pvt in parametersValidatorTypes)
                    {
                        if (invocationContext.Arguments[pvt.Index] is not { } arg)
                        {
                            continue;
                        }

                        var validator = invocationContext.HttpContext.RequestServices.GetRequiredService(
                                pvt.ValidatorType) as IValidator;

                        var validationResult = await validator!.ValidateAsync(new ValidationContext<object>(arg));
                        if (!validationResult.IsValid)
                        {
                            return Results.ValidationProblem(validationResult.ToDictionary());
                        }
                    }

                    return await next(invocationContext);
                };
            });
        });

        return builder;
    }

    private static List<ParameterValidatorType> GetParametersValidatorTypes(EndpointBuilder endpoint)
    {
        var result = new List<ParameterValidatorType>();

        var methodInfo = endpoint.Metadata.OfType<MethodInfo>().FirstOrDefault();
        if (methodInfo is null)
        {
            return result;
        }

        foreach (var p in methodInfo.GetParameters())
        {
            if (ValidatedTypes.TryGetValue(p.ParameterType, out var validatorType))
            {
                result.Add(new ParameterValidatorType(p.Position, validatorType));
            }
        }

        return result;
    }
}

internal record ParameterValidatorType(int Index, Type ValidatorType);
