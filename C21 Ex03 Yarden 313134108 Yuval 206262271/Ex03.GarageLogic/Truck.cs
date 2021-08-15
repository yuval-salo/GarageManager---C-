﻿namespace Ex03.GarageLogic
{
    using System;
    using System.Linq;
    using System.Text;

    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const int k_MaxWheelsAirPressure = 26;
        private const float k_MaxFuelCapacity = 120;
        private const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Soler;
        
        private bool m_IsDrivesHazardousMaterials;
        private float m_MaximumCarryingWeight;

        public Truck(string i_ModelName, string i_LicenseNumber)
          : base(i_ModelName, i_LicenseNumber, k_NumberOfWheels)
        {
            this.m_EnergyManager = new FuelEnergy(k_FuelType, k_MaxFuelCapacity);
        }

        public override void InitWheelsAndEnergy(
            string i_ManufacturerName,
            float i_CurrentAirPressure,
            float i_CurrentEnergy)
        {
            for(int i = 0; i < k_NumberOfWheels; i++) 
            {
                this.m_Wheels.Add(new Wheel());
                this.m_Wheels[i].ManufacturerName = i_ManufacturerName;
                this.m_Wheels[i].MaxAirPressure = k_MaxWheelsAirPressure;
                this.m_Wheels[i].CurrentAirPressure = i_CurrentEnergy;
            }

            ((FuelEnergy)this.m_EnergyManager).AddFuel(i_CurrentEnergy, k_FuelType);
        }

        public bool IsDrivesHazardousMaterials
        {
            get
            {
                return m_IsDrivesHazardousMaterials;
            }

            set
            {
                m_IsDrivesHazardousMaterials = value;
            }
        }

        public float MaximumCarryingWeight
        {
            get
            {
                return m_MaximumCarryingWeight;
            }

            set
            {
                m_MaximumCarryingWeight = value;
            }
        }

        public FuelEnergy.eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        public override string[] GetParamsQuestions()
        {
            StringBuilder truckParamsQuestions = new StringBuilder();
            string[] truckSeparateParamsQuestions = new string[2];

            truckParamsQuestions.AppendFormat(
                @"The truck transporting hazardous materials? ( Y / N ){0}", Environment.NewLine);
            truckSeparateParamsQuestions[0] = truckParamsQuestions.ToString();
            truckParamsQuestions.Clear();

            truckParamsQuestions.AppendFormat(
                @"What is the maximum Carry weight? ( in kilograms ){0}", Environment.NewLine);
            truckSeparateParamsQuestions[1] = truckParamsQuestions.ToString();
            return truckSeparateParamsQuestions;
        }

        public override void InitParams(string i_Params) // maybe better to create operation that this method can get just valid parameters - so we should check it before we use this init method
        {
            string[] givenParams = i_Params.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if(givenParams[0].ToLower() != "y" && givenParams[0].ToLower() != "n")
            {
                // todo : trow exception invalid transport choice 
            }

            m_IsDrivesHazardousMaterials = givenParams[0].ToLower() == "y" ? true : false;

            if (!float.TryParse(givenParams[1], out this.m_MaximumCarryingWeight))
            {
                // todo : trow exception invalid carry weight
            }

            // === ================================================================================================================ ===
            // === if we choose to check the params validation before so just                                                       ===
            // ===                                                                                                                  ===
            // === string[] givenParams = i_Params.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);     ===
            // === m_IsDrivesHazardousMaterials = givenParams[0].ToLower() == "y" ? true : false;                                   ===
            // === float.TryParse(givenParams[1], out this.m_MaximumCarryingWeight);                                                ===
            // ===                                                                                                                  ===
            // === ================================================================================================================ ===
        }
    }
}
