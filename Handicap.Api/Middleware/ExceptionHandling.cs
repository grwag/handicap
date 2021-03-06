﻿using Handicap.Api.Paging;
using Handicap.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Handicap.Api.Middleware
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;

        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(context, ex);
            }
        }

        private static Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            var responseCode = HttpStatusCode.InternalServerError;
            var errorCode = 500;

            if (ex is EntityNotFoundException)
            {
                responseCode = HttpStatusCode.NotFound;
                errorCode = 404;
            }

            if (ex is TenantMissmatchException)
            {
                responseCode = HttpStatusCode.Unauthorized;
                errorCode = 401;
            }

            if (ex is NotEnoughPlayersException)
            {
                responseCode = HttpStatusCode.BadRequest;
                errorCode = 400;
            }

            var result = JsonSerializer.Serialize(
                
                HandicapResponse<ExceptionHandling, ExceptionHandling>.CreateErrorResponse(
                    new HandicapError
                    {
                        ErrorMessage = $"Message: {ex.Message}",
                        ErrorCode = errorCode
                    })
                );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;

            return context.Response.WriteAsync(result);
        }
    }
}
