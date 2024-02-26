using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{

    //public class TuiDayName : 
    /// <summary>
    /// Day of the week
    /// </summary>
    public interface IDayName : 
        IBorderLeftProperty, 
        IBorderTopProperty, 
        IBorderBottomProperty, 
        IBackgroundColorProperty { }

    public interface IDayGrid : 
        IBorderRightProperty,
        IBackgroundColorProperty { }

    public interface IDayGridLeft : 
        IBorderRightProperty,
        IBackgroundColorProperty,
        IWidthProperty { }

    public interface ITimeGrid : IBorderProperty { }

    public interface ITimeGridLeft : 
        IBorderRightProperty, 
        IBackgroundColorProperty, 
        IWidthProperty { }

    public interface ITimeGridLeftAdditionalTimeZone : IBackgroundColorProperty { }

    public interface ITimeGridHalfHour : IBorderBottomProperty { }

    public interface INowIndicatorLabel : IColorProperty { }

    public interface INowIndicatorPast : IBorderProperty { }

    public interface INowIndicatorBullet : IBackgroundColorProperty { }

    public interface INowIndicatorToday : IBorderProperty { }

    public interface INowIndicatorFuture : IBorderProperty { }

    public interface IPastTime : IColorProperty { }

    public interface IFutureTime : IColorProperty { }

    public interface IWeekend : IBackgroundColorProperty { }

    public interface IToday : IColorProperty, IBackgroundColorProperty { }

    public interface IPastDay : IColorProperty { }

    public interface IPanelResizer : IBorderProperty { }

    public interface IGridSelection : IColorProperty { }


    public interface IColorProperty
    {
        [JsonPropertyName("color")]
        public string Color { get; set; }
    }

    public class ColorProperty : IColorProperty
    {
        public string Color { get; set; }
    }

    public interface IBackgroundColorProperty
    {
        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }
    }

    public class BackgroundColorProperty : IBackgroundColorProperty
    {
        public string BackgroundColor { get; set; }
    }

    public interface IBorderProperty
    {
        [JsonPropertyNameAttribute("border")]
        public string Border { get; set; }
    }

    public interface IBorderBottomProperty
    {
        [JsonPropertyName("borderBottom")]
        public string BorderBottom { get; set; }
    }

    public interface IBorderTopProperty
    {
        [JsonPropertyName("borderTop")]
        public string BorderTop { get; set; }
    }

    public interface IBorderLeftProperty
    {
        [JsonPropertyName("borderLeft")]
        public string BorderLeft { get; set; }
    }
    public interface IBorderRightProperty
    {
        [JsonPropertyName("borderRight")]
        public string BorderRight { get; set; }
    }

    public interface IWidthProperty
    {
        [JsonPropertyName("width")]
        public string Width { get; set; }
    }

    #region Properties For Month
    public interface IBoxShadowProperty
    {
        [JsonPropertyName("boxShadow")]
        public string BoxShadow { get; set; }
    }

    public interface IWidthNullableInt
    {
        [JsonPropertyName("Width")]
        public int? Width { get; set; }
    }

    public interface IHeightNullableInt
    {
        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }

    public interface IHeaderHeightNullableInt
    {
        [JsonPropertyName("headerHeight")]
        public int? HeaderHeight { get; set; }
    }

    public interface IFooterHeightNullableInt
    {
        [JsonPropertyName("footerHeight")]
        public int? FooterHeight { get; }
    }
    #endregion


}
