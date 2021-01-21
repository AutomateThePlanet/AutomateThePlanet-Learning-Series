/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Teodor Nikolov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package strategy.services;

import strategy.enums.CouponCode;

public class CouponCodeCalculationService {
    double discount;

    public double calculate(double price, String couponCode) {
        if (couponCode == null) {
            return 0;
        }
        switch(CouponCode.valueOf(couponCode.toUpperCase())) {
            case HAPPYBIRTHDAY:
                discount = 5;
                break;
            default:
                discount = 0;
                break;
        }
        return discount;
    }
}
