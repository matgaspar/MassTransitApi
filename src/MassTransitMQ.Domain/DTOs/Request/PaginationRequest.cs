﻿namespace MassTransitMQ.Domain.DTOs.Request;

public record PaginationRequest(int Page = 1, int PageSize = 10);