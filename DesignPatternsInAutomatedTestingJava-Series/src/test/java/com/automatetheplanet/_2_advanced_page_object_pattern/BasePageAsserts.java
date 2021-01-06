package com.automatetheplanet._2_advanced_page_object_pattern;

public abstract class BasePageAsserts<TM extends BasePageElements> {
    protected abstract TM elements();
}
