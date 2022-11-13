using AutoMapper;
using BanGiay.Models;
using BanGiay.ViewModels;
using System.Runtime.InteropServices;

namespace BanGiay.Helper
{
    public class AutoMap :Profile
    {
        public AutoMap()
        {
            CreateMap<Menu, MenuModels>().ReverseMap();
        }
    }
}
