using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
//using Talent.Service.Paging;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Talent.Service.Utilities
{
    public static class ValueInjectorExtensions
    {
        public static IEnumerable<TTo> MapEnumerable<TTo>(this IEnumerable source)
            where TTo : new()
        {
            var destination = new List<TTo>();
            foreach (var sourceItem in source)
            {
                var item = new TTo();
                item.InjectFrom(sourceItem);
                destination.Add(item);
            }
            return destination;
        }

        public static IEnumerable<TV> MapEnumerable<T, TV>(this IEnumerable<T> source, Action<T, TV> itemFunc = null)
            where TV : new()
        {
            var dest = new List<TV>();

            foreach (var s in source)
            {
                var d = new TV();

                d.InjectFrom(s);

                if (itemFunc != null)
                {
                    itemFunc(s, d);
                }

                dest.Add(d);
            }

            return dest;
        }

        public static IEnumerable<TV> MapEnumerable<T, TV, TArg>(this IEnumerable<T> source, Action<T, TV, TArg> itemFunc = null, TArg arg = default(TArg))
            where TV : new()
        {
            var dest = new List<TV>();

            foreach (var s in source)
            {
                var d = new TV();

                d.InjectFrom(s);

                if (itemFunc != null)
                {
                    itemFunc(s, d, arg);
                }

                dest.Add(d);
            }

            return dest;
        }

        public static IEnumerable<TV> MapEnumerable<T, TV, TArg1, TArg2>(this IEnumerable<T> source, Action<T, TV, TArg1, TArg2> itemFunc = null, TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2))
            where TV : new()
        {
            var dest = new List<TV>();

            foreach (var s in source)
            {
                var d = new TV();

                d.InjectFrom(s);

                if (itemFunc != null)
                {
                    itemFunc(s, d, arg1, arg2);
                }

                dest.Add(d);
            }

            return dest;
        }

        public static TV MapTo<T, TV>(this T source, Action<T, TV> itemFunc = null)
            where TV : new()
        {
            var dest = new TV();

            dest.InjectFrom(source);

            if (itemFunc != null)
            {
                itemFunc(source, dest);
            }

            return dest;
        }

        //public static IPagedList<TTo> MapPagedItems<TTo>(this IPagedList source) where TTo : new()
        //{
        //    var mappedList = source.MapEnumerable<TTo>();
        //    return PagedList<TTo>.FromExisting(mappedList, source.PageIndex, source.PageSize, source.TotalCount);
        //}

        //public static IPagedList<TTo> MapPagedItems<TFrom, TTo>(this IPagedList<TFrom> source, Action<TFrom, TTo> itemFunc = null) where TTo : new()
        //{
        //    var mappedList = source.Items.MapEnumerable(itemFunc);
        //    return PagedList<TTo>.FromExisting(mappedList, source.PageIndex, source.PageSize, source.TotalCount);
        //}


        //public static IPagedList<TTo> MapPagedItems<TFrom, TTo, TArgs>(this IPagedList<TFrom> source, Action<TFrom, TTo, TArgs> itemFunc = null, TArgs args = default(TArgs)) where TTo : new()
        //{
        //    var mappedList = source.Items.MapEnumerable(itemFunc, args);
        //    return PagedList<TTo>.FromExisting(mappedList, source.PageIndex, source.PageSize, source.TotalCount);
        //}


        public static T InjectThis<T>(this T target, object source) where T : new()
        {
            target.InjectFrom(source);
            return target;
        }

        public static T InjectThis<T>(this T target, IValueInjection injection, object source) where T : new()
        {
            target.InjectFrom(injection, source);
            return target;
        }

    }
}
