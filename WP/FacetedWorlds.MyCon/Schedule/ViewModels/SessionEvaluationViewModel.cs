﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
{
    public interface SessionEvaluationViewModel
    {
        bool CanSubmit { get; set; }

        void Submit();
    }
}
