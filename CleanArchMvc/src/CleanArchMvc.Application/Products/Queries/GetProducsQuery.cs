using CleanArchMvc.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CleanArchMvc.Application.Products.Queries
{
    public class GetProducsQuery : IRequest<IEnumerable<Product>>
    {

    }
}