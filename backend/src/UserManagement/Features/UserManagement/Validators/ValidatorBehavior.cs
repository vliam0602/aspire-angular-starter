﻿using FluentValidation;
using MediatR;
using System;
using UserManagement.Features.UserManagement.Validators.CustomException;

namespace UserManagement.Features.UserManagement.Validators;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError
            (
                validationFailure.PropertyName,
                validationFailure.ErrorMessage
            ))
            .ToList();

        if (errors.Any())
        {
            throw new CustomValidationException(errors);
        }

        var response = await next();

        return response;
    }
}
