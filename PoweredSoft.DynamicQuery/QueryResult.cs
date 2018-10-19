﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoweredSoft.DynamicQuery.Core;

namespace PoweredSoft.DynamicQuery
{
    /// <summary>
    /// Represents an aggregate result.
    /// </summary>
    public class AggregateResult : IAggregateResult
    {
        public string Path { get; set; }
        public AggregateType Type { get; set; }
        public object Value { get; set; }
    }

    // part of a result.
    public abstract class QueryResult : IQueryResult
    {
        public List<IAggregateResult> Aggregates { get; set; }
        public List<object> Data { get; set; }

        public bool ShouldSerializeAggregates() => Aggregates != null;
    }

    // not grouped.
    public class QueryExecutionResult : QueryResult, IQueryExecutionResult
    {
        public long TotalRecords { get; set; }
        public long? NumberOfPages { get; set; }
    }

    // grouped.
    public class GroupQueryResult : QueryResult, IGroupQueryResult
    {
        public string GroupPath { get; set; }
        public object GroupValue { get; set; }

        public IEnumerable<IQueryResult> GroupItems => Data.Cast<IQueryResult>();
        public bool ShouldSerializeGroupItems() => false;
    }

    public class GroupedQueryExecutionResult : GroupQueryResult, IQueryExecutionResult
    {
        public long TotalRecords { get; set; }
        public long? NumberOfPages { get; set; }
    }
}