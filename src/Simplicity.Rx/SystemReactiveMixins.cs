﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace System.Reactive.Linq
{
    public static class SystemReactiveMixins
    {
        /// <summary>
        /// Converts an <c>IObservable&lt;T&gt;</c> to an <c>IObservable&lt;Unit&gt;</c>.
        /// </summary>
        /// <returns>An <c>IObservable&lt;Unit&gt;</c></returns>
        /// <param name="source">The source observable.</param>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        public static IObservable<Unit> ToSignal<T>(this IObservable<T> source) => source.Select(_ => Unit.Default);

        /// <summary>
        /// Project every item into an observable and subscribes only to the most recent one. Like <c>SelectMany</c> but uses <c>Switch</c> instead of <c>Merge</c>.
        /// </summary>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        /// <typeparam name="TResult">The type of the resulting observable.</typeparam>
        /// <param name="source">The observable to project.</param>
        /// <param name="selector">The selector to use when projecting each item.</param>
        /// <returns>An <c>IObservable&lt;TResult&gt;</c></returns>
        public static IObservable<TResult> SelectSwitch<T, TResult>(this IObservable<T> source, Func<T, IObservable<TResult>> selector) => source.Select(selector).Switch();

        /// <summary>
        /// Project every item and it's index into an observable and subscribes only to the most recent one. Like <c>SelectMany</c> but uses <c>Switch</c> instead of <c>Merge</c>.
        /// </summary>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        /// <typeparam name="TResult">The type of the resulting observable.</typeparam>
        /// <param name="source">The observable to project.</param>
        /// <param name="selector">The selector to use when projecting each item.</param>
        /// <returns>An <c>IObservable&lt;TResult&gt;</c></returns>
        public static IObservable<TResult> SelectSwitch<T, TResult>(this IObservable<T> source, Func<T, int, IObservable<TResult>> selector) => source.Select(selector).Switch();

        /// <summary>
        /// Catches any error that might occur and return an observable with a single given value.
        /// </summary>
        /// <returns>An observable with a single given value.</returns>
        /// <param name="source">The source observable to catch errors on.</param>
        /// <param name="returnValue">The value to be sent in the returning observable.</param>
        /// <typeparam name="T">The type of the signal inside the observable.</typeparam>
        public static IObservable<T> CatchAndReturn<T>(this IObservable<T> source, T returnValue) => source.Catch(Observable.Return(returnValue));

        /// <summary>
        /// Syntactic sugar for filtering an <c>IObservable&lt;bool&gt;</c> to only contain <c>true</c> or <c>false</c> signals.
        /// </summary>
        /// <returns>An <c>IObservable&lt;bool&gt;</c> that only contains the specified boolean value.</returns>
        /// <param name="source">The observable to filter.</param>
        /// <param name="condition">The boolean value to check against.</param>
        public static IObservable<bool> Where(this IObservable<bool> source, bool condition) => source.Where(value => value == condition);

        /// <summary>
        /// Syntactic sugar for filtering an <c>IObservable&lt;T&gt;</c> to only contain non-null singals.
        /// </summary>
        /// <returns>An <c>IObservable&lt;T&gt;</c> that only contains non-null signals.</returns>
        /// <param name="source">The observable to filter.</param>
        /// <typeparam name="T">The the of the signal inside the observable.</typeparam>
        public static IObservable<T> WhereNotNull<T>(this IObservable<T> source) => source.Where(value => value != null);

        /// <summary>
        /// Writes every signal of the source observable to the debug console (without modifying the source observable)
        /// </summary>
        /// <returns>The source observable.</returns>
        /// <param name="source">The source observable.</param>
        /// <param name="text">A text to prefix the console message with.</param>
        /// <typeparam name="T">The type of the source observable.</typeparam>
		public static IObservable<T> Debug<T>(this IObservable<T> source, string text = "") => source.Do(x => System.Diagnostics.Debug.WriteLine($"{text}{(text.HasValue() ? " " : "")}OnNext: {x}"), ex => System.Diagnostics.Debug.WriteLine($"{text}{(text.HasValue() ? " " : "")}OnError: {ex}"), () => System.Diagnostics.Debug.WriteLine($"{text}{(text.HasValue() ? " " : "")}OnCompleted"));

        /// <summary>
        /// Writes every signal of the source observable to the debug console by applying the selector (without modidying the source observable)
        /// </summary>
        /// <returns>The source observable.</returns>
        /// <param name="source">The source observable.</param>
        /// <param name="selector">A function to select the desired property of the signal.</param>
        /// <param name="text">A text to prefix the console message with.</param>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        /// <typeparam name="TResult">The type of the result of the selector, which is written to the console.</typeparam>
        public static IObservable<T> Debug<T, TResult>(this IObservable<T> source, Func<T, TResult> selector, string text = "") => source.Do(x => System.Diagnostics.Debug.WriteLine($"{text}{(text.HasValue() ? " " : "")}OnNext: {selector(x)}"), ex => System.Diagnostics.Debug.WriteLine($"{text}{(text.HasValue() ? " " : "")}OnError: {ex}"), () => System.Diagnostics.Debug.WriteLine($"{text}{(text.HasValue() ? " " : "")}OnCompleted"));
    }
}
