﻿using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;

namespace Application.Features.WordFeatures.Queries
{
    public class GetUserWordQuery : IRequest<WordCount>
    {
        public string Email { get; set; }
        public GetUserWordQuery(string email)
        {
            Email = email;
        }
        public class GetUserWordQueryHandler : QueriesWithUserWordHandlerBaseClass, IRequestHandler<GetUserWordQuery, WordCount>
        {
            public GetUserWordQueryHandler(IWordsMongoDb context) : base(context)
            {
            }
            public async Task<WordCount> Handle(GetUserWordQuery query, CancellationToken cancellationToken)
            {
                var userWord = await UserWord(query.Email);

                return userWord;
            }
        }
    }
}
