using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailBankSystemClient.Models;
using RetailBankSystemClient.ViewModels;

    public class MFPEDataBase : DbContext
    {
        public MFPEDataBase (DbContextOptions<MFPEDataBase> options)
            : base(options)
        {

        }

        public virtual DbSet<RetailBankSystemClient.Models.CustomerDbo> Customer { get; set; }

       
    }
