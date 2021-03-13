﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;

namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
{
    public class InteractionServiceWriter : DataSupport, IData
    {
        public InteractionServiceWriter(string cText)
        {
            CommandText = cText;
            CommandType = CommandType.Text;    
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}