﻿using Application.Interfaces;
using System;

namespace Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses
{
    public class QueriesHandlerBaseClass
    {
        private protected readonly IWordsDbContext _context;
        public QueriesHandlerBaseClass(IWordsDbContext context)
        {
            _context = context;
        }
        private protected static DateTime DateToday
        {
            get { return DateTime.Today.ToUniversalTime(); }
        }
    }
}
