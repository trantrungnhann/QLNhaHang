﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANQLNH.DTO
{
    public class Menu
    {
        public Menu(string foodName,int count, float price, float totalPrice)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["NAME"].ToString();
            this.Count = (int)row["COUNT"];
            this.Price = (float)Convert.ToDouble(row["GIA"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["TongTien"].ToString());
        }


        private string foodName;
        private int count;
        private float price;
        private float totalPrice;

        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
