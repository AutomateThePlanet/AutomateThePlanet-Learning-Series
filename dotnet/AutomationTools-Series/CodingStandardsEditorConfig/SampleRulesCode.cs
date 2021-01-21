// <copyright file="SampleRulesCode.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://automatetheplanet.com/</site>

// 16.  Sort System Directives First

// dotnet_sort_system_directives_first = false
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//// dotnet_sort_system_directives_first = true
////using System;
////using System.Collections.Generic;
namespace CodingStandardsEditorConfig
{
    public class SampleRulesCode
    {
        private delegate void SpaceshipFire(string fuelType);

        public void TestMethod()
        {
            // 1. Indent Case Content
            bool isEarthRound = false;

            // csharp_indent_block_contents = true
            if (isEarthRound)
            {
                var answerOfUniverse = 42;
            }

            // csharp_indent_block_contents = false
            if (isEarthRound)
            {
                var answerOfUniverse = 42;
            }

            // 2. Indent Block Contents
            var answerOfEverything = 42;

            // csharp_indent_case_contents = true
            switch (answerOfEverything)
            {
                case 42:
                    break;
                case 13:
                    break;
            }

            // csharp_indent_case_contents = false
            switch (answerOfEverything)
            {
                case 42:
                    break;
                case 13:
                    break;
            }

            // 3. Prefer Braces

            // csharp_prefer_braces = true:error
            if (isEarthRound)
            {
                return;
            }

            // csharp_prefer_braces = false:error
            ////if (isEarthRound)
            //    return;

            // 4. Space After Keywords in Control Flow Statements

            // csharp_space_after_keywords_in_control_flow_statements = true
            if (isEarthRound)
            {
            }

            while (isEarthRound)
            {
            }

            // csharp_space_after_keywords_in_control_flow_statements = false
            if (isEarthRound)
            {
            }

            while (isEarthRound)
            {
            }

            SpaceshipFire spaceshipOne = (s) =>
            {
                System.Console.WriteLine(s);
            };

            // 6. Style Conditional Delegate Call
            // csharp_style_conditional_delegate_call = true:error
            spaceshipOne?.Invoke("98");

            // csharp_style_conditional_delegate_call = false:error
            if (spaceshipOne != null)
            {
                spaceshipOne("98");
            }

            // 12. Style Throw Expression

            // csharp_style_throw_expression = true:error
            FuelType = _fuelType ?? throw new ArgumentNullException(nameof(FuelType));

            // csharp_style_throw_expression = false:error
            if (_fuelType == null)
            {
                throw new ArgumentNullException(nameof(_fuelType));
            }

            FuelType = _fuelType;

            // 13. Style var Elsewhere

            // csharp_style_var_elsewhere = true:error
            int moonSize = CalculateMoonSize();

            // csharp_style_var_elsewhere = false:error
            var secondMoonSize = CalculateMoonSize();

            // 14. Style var for Built in Types

            // csharp_style_var_for_built_in_types = true:error
            var planetSize = 42;

            // csharp_style_var_for_built_in_types = false:error
            int secondPlanetSize = 42;

            // 15. Style var When Type Is Apparent

            // csharp_style_var_when_type_is_apparent = true:error
            var robots = new List<int>();

            // csharp_style_var_when_type_is_apparent = false:error
            ////List<int> robots = new List<int>();

            // 17. Style Coalesce Expression

            // dotnet_style_coalesce_expression = true:error
            int? sunSize = null;
            var blackHole = sunSize ?? 10000;

            // dotnet_style_coalesce_expression = false:error
            var secondBlackHole = sunSize != null ? sunSize : 10000;

            // 18. Style Collection Initializer

            // dotnet_style_collection_initializer = true:error
            var shieldsPower = new List<int> { 42, 13 };

            // dotnet_style_collection_initializer = false:error
            ////var shieldsPower = new List<int>();
            ////shieldsPower.Add(42);
            ////shieldsPower.Add(13);

            // 19. Style null Propagation
            var spaceShipFactory = new SpaceShipFactory();

            // dotnet_style_null_propagation = true:error
            var smallSpaceShip = spaceShipFactory?.Build();

            // dotnet_style_null_propagation = false:error
            ////var smallSpaceShip = spaceShipFactory == null ? null : spaceShipFactory.Build();

            // 20. Style Object Initializer

            // dotnet_style_object_initializer = true:error
            var bigSpaceShip = new SpaceShip
            {
                Name = "Meissa",
            };

            // dotnet_style_object_initializer = false:error
            ////var bigSpaceShip = new SpaceShip();
            ////bigSpaceShip.Name = "Meissa";

            // 22. Prefer NOT to use this.

            //// dotnet_style_qualification_for_event = false:error
            ////FireRocket += OnFireRocket;
            //// dotnet_style_qualification_for_field = false:error
            ////rocketSize = 42;
            //// dotnet_style_qualification_for_method = false:error
            ////FireRocket();
            //// dotnet_style_qualification_for_property = false:error
            ////RocketSize = 42;

            //// dotnet_style_qualification_for_event = true:error
            ////this.FireRocket += OnFireRocket;
            //// dotnet_style_qualification_for_field = true:error
            ////this.rocketSize = 42;
            //// dotnet_style_qualification_for_method = true:error
            ////this.FireRocket();
            //// dotnet_style_qualification_for_property = true:error
            ////this.RocketSize = 42;
        }

        // 21. Style Predefined Type

        //// dotnet_style_predefined_type_for_member_access = true:error
        ////var ringSize = int.MinValue;

        //// dotnet_style_predefined_type_for_locals_parameters_members = true:error
        ////public void MeasureRingSize(int width)
        ////{
        ////}

        //// dotnet_style_predefined_type_for_member_access = false:error
        ////var ringSize = Int32.MinValue;
        //// dot-net_style_predefined_type_for_locals_parameters_members = false:error
        ////public void MeasureRingSize(Int32 width)
        ////{
        ////}
        private int CalculateMoonSize()
        {
            throw new NotImplementedException();
        }

        // 7. Style Expression Bodied Accessors
        // csharp_style_expression_bodied_accessors = true:error
        private int? _fuelType;
        public int? FuelType
        {
            get => _fuelType;
            set => _fuelType = value;
        }

        // csharp_style_expression_bodied_accessors = false:error
        private int _fuelType1;
        public int FuelType1
        {
            get { return _fuelType1; }
            set { _fuelType1 = value; }
        }

        // 8. Style Expression Bodied Constructors

        // csharp_style_expression_bodied_constructors = true:error
        ////public SampleRulesCode() => FuelType1 = 100;

        // csharp_style_expression_bodied_constructors = false:error
        public SampleRulesCode()
        {
            FuelType1 = 100;
        }

        // 9. Style Expression Bodied Indexers

        // csharp_style_expression_bodied_indexers = true:error
        public int this[int i] => 42;

        // csharp_style_expression_bodied_indexers = false:error
        ////public int this[int i]
        ////{
        ////    get { return 42; }
        ////}

        // 10. Style Expression Bodied Methods

        // csharp_style_expression_bodied_methods = true:error
        public int CalculateAnswerOfEverything() => 42;

        // csharp_style_expression_bodied_methods = false:error
        ////public int CalculateAnswerOfEverything()
        ////{
        ////    return 42;
        ////}

        // 11. Style Expression Bodied Properties

        // csharp_style_expression_bodied_properties = true:error
        public int PlanetsCount => 42;

        // csharp_style_expression_bodied_properties = false:error
        public int MoonsCount
        {
            get { return 13; }
        }
    }

    // 5. Space before Colon in Inheritance Clause

    //// csharp_space_before_colon_in_inheritance_clause = true
    ////public class SpaceShipOne : Rocket

    //// csharp_space_before_colon_in_inheritance_clause = false
    ////public class SpaceShipOne: Rocket
}
