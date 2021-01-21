// <copyright file="TypeFourFlower.cs" company="Automate The Planet Ltd.">
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
    public class TypeFourFlower : Flower
    {
        public TypeFourFlower(ConsoleColor color, ConsoleKey letter) : base(color, letter)
        {
        }

        public override string FlowerPicture
        {
            get
            {
                return @"
      .=====__
                    /==Z' .===_ ~~=,_===\
                  _/  |   |    YZ, `\,   ~\
                  |   |   | _/=j'\   !,   d
           __====_|   |   b/    V`;  /'  .M ,
   `5\==/~~       W,  t   d+,  .D4| /   /'|/~~~\=__     .-
    `\            t~\ |   |t`~~T/'|Z  :/  |        ~~\=/V
      \           |  \4,  | ~/~' :Z  -!   |             |
       \,      /\__|   \\.!     :XG   \   / ._,       ./'
        `L    |    ~;    V;  _//' |    \ .f~' `~;    .b_
       ./ \\__JL    `;    Y7~     |    / /     d   //'  \,
      .!       `D\,  `\,   |     .!   .t/    .(_/=~      \
      /         `;`~~~=+=qLb,   jK_L==f'    j''          `;
    ./          .(r,        `~\5'   ~\\,._r/              |
 ~=m!         ./D' `\,          \,     !G~                 t
    ~==___===/'/   .!`\__       /! __=~\\~=_                TG=
              |   .|     ~\=\=r@/~5 \   !,  ~=_,        __//'
              |./~V           ||  `| \,  t     ~~~~\==~~
              t|  |           | |  |  !\, \=_,
              !   t          .! !, \    `\/~~~
                  |          /   !\/\
                  `;       ./      `~-
                   t      .!
                    N,  ./'
                     `\/'
";
            }
        }
    }
}
