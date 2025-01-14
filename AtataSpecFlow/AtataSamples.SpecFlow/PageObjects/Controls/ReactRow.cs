﻿using Atata;

namespace AtataSamples.SpecFlow.PageObjects.Controls
{
    //[ControlDefinition("div", ContainingClass = "rt-tr", ComponentTypeName = "row")]
    [ControlDefinition("div", ContainingClass = "rt-tr-group", ComponentTypeName = "rowgroup")]
    public class ReactRow<TOwner> : TableRow<TOwner>
        where TOwner : PageObject<TOwner>
    {
        //[FindByXPath("//div[@role='gridcell'][1]")]
        [FindByCss(".rt-td:nth-of-type(1)")]
        public Text<TOwner> FirstName { get; private set; }

        //[FindByXPath("//div[@role='gridcell'][2]")]
        [FindByCss(".rt-td:nth-of-type(2)")]
        public Text<TOwner> LastName { get; private set; }

        //[FindByXPath("//div[@role='gridcell'][3]")]
        [FindByCss(".rt-td:nth-of-type(3)")]
        public Text<TOwner> Age { get; private set; }

        //[FindByXPath("//div[@role='gridcell'][4]")]
        [FindByCss(".rt-td:nth-of-type(4)")]
        public Text<TOwner> Email { get; private set; }

        //[FindByXPath("//div[@role='gridcell'][5]")]
        [FindByCss(".rt-td:nth-of-type(5)")]
        public Text<TOwner> Salary { get; private set; }

        //[FindByXPath("//div[@role='gridcell'][6]")]
        [FindByCss(".rt-td:nth-of-type(6)")]
        public Text<TOwner> Department { get; private set; }

        //[FindByXPath("//div[@role='gridcell'][7]")]
        //[FindByCss(".rt-td:nth-of-type(7)")]
        //public Text<TOwner> Action { get; private set; }

        [FindByXPath("//span[@title='Delete']")]
        public Svg<TOwner> Delete { get; private set; }

        [FindByXPath("//span[@title='Edit']")]
        public Svg<TOwner> Edit { get; private set; }
    }
}
