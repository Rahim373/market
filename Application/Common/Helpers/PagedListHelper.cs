using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using Market.Application.Models;
using Market.Domain.Exceptions;

namespace Market.Application.Helpers
{
    public class PagedListHelper<T>
    {
        private readonly IQueryable<T> _query;

        public PagedListHelper(IQueryable<T> query)
        {
            this._query = query;
        }

        public GridResponseViewModel<TMapping> ToPagedList<TMapping>(GridFilterViewModel filter)
        {
            var response = new GridResponseViewModel<TMapping>();

            try
            {
                response.TotalCount = _query.Count();
                response.PageLength = filter.PageLength;
                decimal totalCount = response.TotalCount;
                response.TotalPages = (int) Math.Ceiling(totalCount / response.PageLength);
                response.PageNumber = filter.PageNumber;

                var skip = response.PageLength * (response.PageNumber - 1);
                var records = _query
                    .Skip(skip)
                    .Take(response.PageLength)
                    .ToList();

                response.Items = records.Adapt<List<TMapping>>();
            }
            catch (Exception e)
            {
                throw new InvalidQueryException("Invalid query.", e);
            }

            return response;
        }
    }
}