// <copyright file="TypeFiveFlower.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using System;

namespace FlowersSurprise.Console.Flowers
{
    public class TypeFiveFlower : Flower
    {
        public TypeFiveFlower(ConsoleColor color, ConsoleKey letter) : base(color, letter)
        {
        }

        public override string FlowerPicture
        {
            get
            {
                return @"
    .@.                                    .
              @m@,.                                 .@
             .@m%nm@,.                            .@m@
            .@nvv%vnmm@,.                      .@mn%n@
           .@mnvvv%vvnnmm@,.                .@mmnv%vn@,
           @mmnnvvv%vvvvvnnmm@,.        .@mmnnvvv%vvnm@
           @mmnnvvvvv%vvvvvvnnmm@, ;;;@mmnnvvvvv%vvvnm@,
           `@mmnnvvvvvv%vvvvvnnmmm;;@mmnnvvvvvv%vvvvnmm@
            `@mmmnnvvvvvv%vvvnnmmm;%mmnnvvvvvv%vvvvnnmm@
              `@m%v%v%v%v%v;%;%;%;%;%;%;%%%vv%vvvvnnnmm@
              .,mm@@@@@mm%;;@@m@m@@m@@m@mm;;%%vvvnnnmm@;@,.
           .,@mmmmmmvv%%;;@@vmvvvvvvvvvmvm@@;;%%vvnnm@;%mmm@,
        .,@mmnnvvvvv%%;;@@vvvvv%%%%%%%vvvvmm@@;;%%mm@;%%nnnnm@,
     .,@mnnvv%v%v%v%%;;@mmvvvv%%;*;*;%%vvvvmmm@;;%m;%%v%v%v%vmm@,.
 ,@mnnvv%v%v%v%v%v%v%;;@@vvvv%%;*;*;*;%%vvvvm@@;;m%%%v%v%v%v%v%vnnm@,
 `    `@mnnvv%v%v%v%%;;@mvvvvv%%;;*;;%%vvvmmmm@;;%m;%%v%v%v%vmm@'   '
         `@mmnnvvvvv%%;;@@mvvvv%%%%%%%vvvvmm@@;;%%mm@;%%nnnnm@'
            `@mmmmmmvv%%;;@@mvvvvvvvvvvmmm@@;;%%mmnmm@;%mmm@'
               `mm@@@@@mm%;;@m@@m@m@m@@m@@;;%%vvvvvnmm@;@'
              ,@m%v%v%v%v%v;%;%;%;%;%;%;%;%vv%vvvvvnnmm@
            .@mmnnvvvvvvv%vvvvnnmm%mmnnvvvvvvv%vvvvnnmm@
           .@mmnnvvvvvv%vvvvvvnnmm'`@mmnnvvvvvv%vvvnnmm@
           @mmnnvvvvv%vvvvvvnnmm@':%::`@mmnnvvvv%vvvnm@'
           @mmnnvvv%vvvvvnnmm@'`:::%%:::'`@mmnnvv%vvmm@
           `@mnvvv%vvnnmm@'     `:;%%;:'     `@mvv%vm@'
            `@mnv%vnnm@'          `;%;'         `@n%n@
             `@m%mm@'              ;%;.           `@m@
              @m@'                 `;%;             `@
              `@'                   ;%;.             '
";
            }
        }
    }
}
