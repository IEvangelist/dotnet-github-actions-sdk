// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient;

public interface IRequestHandler
{
    void PrepareRequest(RequestOptions options);
}
