﻿using AutoMapper;
using Entities;
using DataTransferObjects.BundleDTOs;
using DataTransferObjects.DepartmentDTOs;
using DataTransferObjects.ProductDTOs;
using DataTransferObjects.ProductTypeDTOs;
using DataTransferObjects.UserDTOs;
using DataTransferObjects.UserItemDTOs;
using DataTransferObjects.UserPerkDTOs;

namespace Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDTO, UserEntity>().ReverseMap();
            CreateMap<UpdateUserDTO, UserEntity>().ReverseMap();
            CreateMap<GetUserDTO,    UserEntity>().ReverseMap();

            CreateMap<CreateProductDTO,       ProductEntity>().ReverseMap();
            CreateMap<UpdateProductDTO,       ProductEntity>().ReverseMap();
            CreateMap<GetProductDTO,          ProductEntity>().ReverseMap();
            CreateMap<GetAvailableProductDTO, ProductEntity>().ReverseMap();

            CreateMap<CreateDepartmentDTO, DepartmentEntity>().ReverseMap();
            CreateMap<UpdateDepartmentDTO, DepartmentEntity>().ReverseMap();
            CreateMap<GetDepartmentDTO,    DepartmentEntity>().ReverseMap();

            CreateMap<CreateProductTypeDTO, ProductTypeEntity>().ReverseMap();
            CreateMap<UpdateProductTypeDTO, ProductTypeEntity>().ReverseMap();
            CreateMap<GetProductTypeDTO,    ProductTypeEntity>().ReverseMap();

            CreateMap<CreateBundleDTO, BundleEntity>().ReverseMap();
            CreateMap<UpdateBundleDTO, BundleEntity>().ReverseMap();
            CreateMap<GetBundleDTO,    BundleEntity>().ReverseMap();

            CreateMap<CreateUserItemDTO,     UserItemEntity>().ReverseMap();
            CreateMap<GetUserItemDTO,        UserItemEntity>().ReverseMap();
            CreateMap<GetNotReceivedItemDTO, UserItemEntity>().ReverseMap();

            CreateMap<CreateUserPerkDTO, UserPerkEntity>().ReverseMap();
            CreateMap<GetUserPerkDTO, UserPerkEntity>().ReverseMap();
        }
    }
}
