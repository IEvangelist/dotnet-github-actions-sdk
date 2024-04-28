﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient;

/// <summary>
/// Represents an HTTP client abstraction, exposing Native AOT compatible APIs.
/// </summary>
public interface IHttpClient : IDisposable
{
    /// <summary>
    /// Sends an HTTP <c>OPTIONS</c> request to the specified <paramref name="requestUri"/>.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>An <see cref="HttpResponseMessage"/> from the underlying request.</returns>
    ValueTask<HttpResponseMessage> OptionsAsync(
        string requestUri,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP <c>GET</c> request to the specified <paramref name="requestUri"/>.
    /// </summary>
    /// <typeparam name="T">The target type to deserialize to.</typeparam>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="jsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A <see cref="TypedResponse{TResult}"/> where <c>TResult</c> is of type <typeparamref name="T"/>.</returns>
    ValueTask<TypedResponse<T>> GetAsync<T>(
        string requestUri,
        JsonTypeInfo<T> jsonTypeInfo,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP <c>DELETE</c> request to the specified <paramref name="requestUri"/>.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>An <see cref="HttpResponseMessage"/> from the underlying request.</returns>
    ValueTask<HttpResponseMessage> DeleteAsync(
        string requestUri,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP <c>POST</c> request to the specified <paramref name="requestUri"/>,
    /// with the given <paramref name="data"/> as the request body.
    /// </summary>
    /// <typeparam name="TData">The data type to deserialize as the POST body.</typeparam>
    /// <typeparam name="TResult">The target resulting type to deserialize the response as.</typeparam>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="data"></param>
    /// <param name="dataJsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="resultJsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A <see cref="TypedResponse{TResult}"/> where <c>TResult</c> is of type <typeparamref name="TResult"/>.</returns>
    ValueTask<TypedResponse<TResult>> PostAsync<TData, TResult>(
        string requestUri,
        TData data,
        JsonTypeInfo<TData> dataJsonTypeInfo,
        JsonTypeInfo<TResult> resultJsonTypeInfo,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP <c>PATCH</c> request to the specified <paramref name="requestUri"/>,
    /// with the given <paramref name="data"/> as the request body.
    /// </summary>
    /// <typeparam name="TData">The data type to deserialize as the POST body.</typeparam>
    /// <typeparam name="TResult">The target resulting type to deserialize the response as.</typeparam>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="data"></param>
    /// <param name="dataJsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="resultJsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A <see cref="TypedResponse{TResult}"/> where <c>TResult</c> is of type <typeparamref name="TResult"/>.</returns>
    ValueTask<TypedResponse<TResult>> PatchAsync<TData, TResult>(
        string requestUri,
        TData data,
        JsonTypeInfo<TData> dataJsonTypeInfo,
        JsonTypeInfo<TResult> resultJsonTypeInfo,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP <c>PUT</c> request to the specified <paramref name="requestUri"/>,
    /// with the given <paramref name="data"/> as the request body.
    /// </summary>
    /// <typeparam name="TData">The data type to deserialize as the POST body.</typeparam>
    /// <typeparam name="TResult">The target resulting type to deserialize the response as.</typeparam>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="data"></param>
    /// <param name="dataJsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="resultJsonTypeInfo">The <see cref="JsonTypeInfo" /> used to control the behavior during deserialization.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A <see cref="TypedResponse{TResult}"/> where <c>TResult</c> is of type <typeparamref name="TResult"/>.</returns>
    ValueTask<TypedResponse<TResult>> PutAsync<TData, TResult>(
        string requestUri,
        TData data,
        JsonTypeInfo<TData> dataJsonTypeInfo,
        JsonTypeInfo<TResult> resultJsonTypeInfo,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP <c>HEAD</c> request to the specified <paramref name="requestUri"/>.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="additionalHeaders">A set of additional HTTP request headers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>An <see cref="HttpResponseMessage"/> from the underlying request.</returns>
    ValueTask<HttpResponseMessage> HeadAsync(
        string requestUri,
        Dictionary<string, IEnumerable<string>>? additionalHeaders = null,
        CancellationToken cancellationToken = default);
}
