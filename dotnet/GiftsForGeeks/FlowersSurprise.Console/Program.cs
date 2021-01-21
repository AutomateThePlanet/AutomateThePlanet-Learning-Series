// <copyright file="Program.cs" company="Automate The Planet Ltd.">
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

using FlowersSurprise.Console.Flowers;
using System;
using System.Collections.Generic;

namespace FlowersSurprise.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Version 1.0
            ////            string flower1 = @"
            ////             H           H   H   H           H
            ////                        H   H       H   H   H   H       H   H
            ////                          H   H       H   H   H       H   H
            ////                H       H   H       H   H   H   H       H   H       H
            ////              H   H       H   H       H   H   H       H   H       H   H
            ////                H   H   H   H   H       H   H       H   H   H   H   H
            ////              H   H       H   H   H       H       H   H   H       H   H
            ////                H   H       H   H   H           H   H   H   H   H   H
            ////              H   H   H       H   H   H       H   H   H   H       H   H
            ////@               H   H   H       H   H   H   H   H   H   H       H   H
            //// @ @          H   H   H   H       H   H   H   H   H   H       H   H   H
            ////    @ @         H   H   H   H   H   H   H   H   H   H   H       H   H
            ////     @ @ @        H   H   H       H   H   H   H   H   H       H   H   H
            ////        @ @     H   H   H   H       H   H   H   H   H       H   H   H
            ////         @ @ @    H   H   H           H   H   H   H       H   H   H    @ @
            ////          @ @   H   H   H   H   @   H   H   H           H   H   H   @ @ @ @ @
            ////         @ @      H   H   H    @ @    H   H           H   H   H    @ @ @   @ @
            ////        @ @ @   H   H   H   H   @ @     H       H   H   H   H     @ @ @
            ////         @ @  H   H   H   H      @ @          H   H   H   H   H    @ @ @
            ////          @     H   H   H   H   @ @ @   H   H   H   H   H   H     @ @ @
            ////              H   H   H   H      @ @      H   H   H   H   H   H    @ @
            ////                H   H   H   H     @ @       H   H   H   H   H     @ @ @
            ////     @ @ @        H   H   H   H    @ @    H   H   H   H   H   H    @ @
            ////  @ @ @ @ @ @       H   H   H     @ @ @     H   H   H   H   H     @ @ @
            //// @ @     @ @ @        H   H   H    @ @ @      H   H   H   H      @ @ @
            ////  @       @ @ @ @   H   H   H   H   @ @ @   H   H   H   H   H   @ @ @
            ////             @ @ @    H   H   H    @ @ @ @    H   H   H   H    @ @ @ @
            ////              @ @ @     H   H   H   @ @ @   H   H   H   H     @ @ @
            ////                 @ @ @    H   H    @ @ @ @    H   H   H      @ @
            ////                    @ @     H   H   @ @ @ @     H   H       @ @ @
            ////                     @ @ @    H    @ @ @ @ @  H          @ @ @
            ////                      @ @ @       @ @ @ @ @ @   @ @ @ @ @ @ @
            ////                       @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @ @
            ////                          @ @ @ @ @ @ @ @ @ @ @ @ @ @ @
            ////                             @ @ @ @ @ @ @ @ @ @
            ////                                  @ @ @ @ @ @
            ////                                   @ @ @ @ @
            ////                                      @ @ @
            ////             @ @ @                   @ @ @
            ////              @ @ @ @ @ @             @ @ @
            ////               @ @ @ @ @ @ @         @ @ @
            ////                @ @ @ @ @ @ @       @ @ @
            ////                 @ @ @ @ @ @ @     @ @ @   @ @ @ @ @
            ////                    @ @ @ @ @   @ @ @ @ @ @ @ @ @ @ @ @ @
            ////                         @ @   @ @ @     @ @ @ @ @ @ @ @ @
            ////                            @ @ @ @       @ @ @ @ @ @ @ @ @
            ////                         @ @ @ @             @ @ @ @ @
            ////                      @ @ @ @
            ////                   @ @ @ @
            ////";

            ////            string flower2 = @"
            ////     %%%%%%
            ////         %%%%%% 
            ////           &&%%%%%
            ////            '   %%%%%
            ////	     '    %%%%
            ////             ''      %%%%
            ////             ' '       %%%%
            ////             '  '        %%%%
            ////	    '    '         %%%%
            ////	   '  ~~   '          %%%%
            ////	  ' ~~ ~~  '	        %%%%
            ////	' ~~~  ~~~  '             %%%%
            ////      ' ~~~~~  ~~~~ '              %%%%
            ////     ' ~~~~~ ~~~~~  '              %%%%
            ////    ' ~~~~~  ~~~~  '	         &&&&&&&
            ////    `  ~~~  ~~~  '	       {{}}{{}}{{}}
            ////    `  ~~~ ~~  '                /         \
            ////     `  ~~~  '                /     /\      \
            ////      `    '                /                 \
            ////   	`'                /       /    \        \
            ////                        /                         \
            ////	              /         /        \         \
            ////                     /                              \
            ////                     \       /   \      /    \      /
            ////                       ....        ....        ....
            ////	                     //     ||     \\
            //// 	        	    //      ||      \\
            ////   	        	   //       ||       \\
            ////                          o o       oo        o o
            ////                          oo        oo         oo
            ////                           o         o          o
            ////";

            ////            string flower3 = @"
            ////   __ .---.
            ////            __ /  `  .-.7,--.
            ////           /  `. .-''. -,  , \
            ////           '--.-    -;   | ) /
            ////           ,` /   \ ,_) /   '-.
            ////          /  (  (  |   /  .' ) \
            ////          '.  `--,/   .---' ,-.|
            ////            `--.  / '-, -' .'
            ////           .==,=; `-,.;--'
            ////          / ,'  _;--;|
            ////         /_...='    ||
            ////                jgs || .==,=.
            ////                    ||/    '.\
            ////                   ,||`'=...__\
            ////                    ||
            ////                    ||
            ////                    ||,
            ////                    ||
            ////                    ||
            ////                    ||

            ////";

            ////            string flower4 = @"
            ////      .=====__
            ////                    /==Z' .===_ ~~=,_===\
            ////                  _/  |   |    YZ, `\,   ~\
            ////                  |   |   | _/=j'\   !,   d
            ////           __====_|   |   b/    V`;  /'  .M ,
            ////   `5\==/~~       W,  t   d+,  .D4| /   /'|/~~~\=__     .-
            ////    `\            t~\ |   |t`~~T/'|Z  :/  |        ~~\=/V
            ////      \           |  \4,  | ~/~' :Z  -!   |             |
            ////       \,      /\__|   \\.!     :XG   \   / ._,       ./'
            ////        `L    |    ~;    V;  _//' |    \ .f~' `~;    .b_
            ////       ./ \\__JL    `;    Y7~     |    / /     d   //'  \,
            ////      .!       `D\,  `\,   |     .!   .t/    .(_/=~      \
            ////      /         `;`~~~=+=qLb,   jK_L==f'    j''          `;
            ////    ./          .(r,        `~\5'   ~\\,._r/              |
            //// ~=m!         ./D' `\,          \,     !G~                 t
            ////    ~==___===/'/   .!`\__       /! __=~\\~=_                TG=
            ////              |   .|     ~\=\=r@/~5 \   !,  ~=_,        __//'
            ////              |./~V           ||  `| \,  t     ~~~~\==~~
            ////              t|  |           | |  |  !\, \=_,
            ////              !   t          .! !, \    `\/~~~
            ////                  |          /   !\/\
            ////                  `;       ./      `~-
            ////                   t      .!
            ////                    N,  ./'
            ////                     `\/'";

            ////            string flower5 = @"
            ////    .@.                                    .
            ////              @m@,.                                 .@
            ////             .@m%nm@,.                            .@m@
            ////            .@nvv%vnmm@,.                      .@mn%n@
            ////           .@mnvvv%vvnnmm@,.                .@mmnv%vn@,
            ////           @mmnnvvv%vvvvvnnmm@,.        .@mmnnvvv%vvnm@
            ////           @mmnnvvvvv%vvvvvvnnmm@, ;;;@mmnnvvvvv%vvvnm@,
            ////           `@mmnnvvvvvv%vvvvvnnmmm;;@mmnnvvvvvv%vvvvnmm@
            ////            `@mmmnnvvvvvv%vvvnnmmm;%mmnnvvvvvv%vvvvnnmm@
            ////              `@m%v%v%v%v%v;%;%;%;%;%;%;%%%vv%vvvvnnnmm@
            ////              .,mm@@@@@mm%;;@@m@m@@m@@m@mm;;%%vvvnnnmm@;@,.
            ////           .,@mmmmmmvv%%;;@@vmvvvvvvvvvmvm@@;;%%vvnnm@;%mmm@,
            ////        .,@mmnnvvvvv%%;;@@vvvvv%%%%%%%vvvvmm@@;;%%mm@;%%nnnnm@,
            ////     .,@mnnvv%v%v%v%%;;@mmvvvv%%;*;*;%%vvvvmmm@;;%m;%%v%v%v%vmm@,.
            //// ,@mnnvv%v%v%v%v%v%v%;;@@vvvv%%;*;*;*;%%vvvvm@@;;m%%%v%v%v%v%v%vnnm@,
            //// `    `@mnnvv%v%v%v%%;;@mvvvvv%%;;*;;%%vvvmmmm@;;%m;%%v%v%v%vmm@'   '
            ////         `@mmnnvvvvv%%;;@@mvvvv%%%%%%%vvvvmm@@;;%%mm@;%%nnnnm@'
            ////            `@mmmmmmvv%%;;@@mvvvvvvvvvvmmm@@;;%%mmnmm@;%mmm@'
            ////               `mm@@@@@mm%;;@m@@m@m@m@@m@@;;%%vvvvvnmm@;@'
            ////              ,@m%v%v%v%v%v;%;%;%;%;%;%;%;%vv%vvvvvnnmm@
            ////            .@mmnnvvvvvvv%vvvvnnmm%mmnnvvvvvvv%vvvvnnmm@
            ////           .@mmnnvvvvvv%vvvvvvnnmm'`@mmnnvvvvvv%vvvnnmm@
            ////           @mmnnvvvvv%vvvvvvnnmm@':%::`@mmnnvvvv%vvvnm@'
            ////           @mmnnvvv%vvvvvnnmm@'`:::%%:::'`@mmnnvv%vvmm@
            ////           `@mnvvv%vvnnmm@'     `:;%%;:'     `@mvv%vm@'
            ////            `@mnv%vnnm@'          `;%;'         `@n%n@
            ////             `@m%mm@'              ;%;.           `@m@
            ////              @m@'                 `;%;             `@
            ////              `@'                   ;%;.             '
            ////";
            
            //var key = new ConsoleKeyInfo();

            //while (!System.Console.KeyAvailable && key.Key != ConsoleKey.Escape)
            //{
            //    System.Console.ForegroundColor = ConsoleColor.White;
            //    System.Console.WriteLine();
            //    System.Console.Write("Enter a letter part of your name (Qwert) (quit = ESC): ");
            //    key = System.Console.ReadKey(true);

            //    switch (key.Key)
            //    {
            //        case ConsoleKey.Q:
            //            System.Console.ForegroundColor = ConsoleColor.Red;
            //            System.Console.WriteLine(flower1);
            //            break;
            //        case ConsoleKey.W:
            //            System.Console.ForegroundColor = ConsoleColor.Blue;
            //            System.Console.WriteLine(flower2);
            //            break;

            //        case ConsoleKey.E:
            //            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            //            System.Console.WriteLine(flower3);
            //            break;

            //        case ConsoleKey.R:
            //            System.Console.ForegroundColor = ConsoleColor.DarkCyan;
            //            System.Console.WriteLine(flower4);
            //            break;

            //        case ConsoleKey.T:
            //            System.Console.ForegroundColor = ConsoleColor.DarkGray;
            //            System.Console.WriteLine(flower5);
            //            break;

            //        default:
            //            if (System.Console.CapsLock && System.Console.NumberLock)
            //            {
            //                System.Console.WriteLine(key.KeyChar);
            //            }
            //            break;
            //    }
            //}

            // Version 2.0
            var flowers = new List<Flower>()
            {
                new TypeOneFlower(ConsoleColor.Red, ConsoleKey.Q),
                new TypeTwoFlower(ConsoleColor.Blue, ConsoleKey.W),
                new TypeThreeFlower(ConsoleColor.DarkYellow, ConsoleKey.E),
                new TypeFourFlower(ConsoleColor.DarkCyan, ConsoleKey.R),
                new TypeFiveFlower(ConsoleColor.DarkGray, ConsoleKey.T),
            };

            FlowersExecutionEngine.Execute(flowers);
        }
    }
}
