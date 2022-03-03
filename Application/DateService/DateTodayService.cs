using Application.Interfaces;
using System;

namespace Application.DateService
{
    public class DateTodayService : IDateTodayService
    {
        public DateTime DateToday
        {
            get { return DateTime.Today.ToUniversalTime(); }
        }
    }
}
