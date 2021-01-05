using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ScheduleXamarin
{
    public class SchedulerBehavior : Behavior<ContentPage>
    {
        SfSchedule schedule;
        ToolbarItem day, week;
        DateTime? minDate, maxDate;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            schedule = bindable.FindByName<SfSchedule>("schedule");
            day = bindable.FindByName<ToolbarItem>("Day");
            week = bindable.FindByName<ToolbarItem>("Week");

            schedule.VisibleDatesChangedEvent += Schedule_VisibleDatesChangedEvent;

            day.Clicked += Day_Clicked;
            week.Clicked += Week_Clicked;
        }
        private void Schedule_VisibleDatesChangedEvent(object sender, VisibleDatesChangedEventArgs e)
        {
            if (schedule.ScheduleView == ScheduleView.WeekView)
            {
                minDate = e.visibleDates[0].Date;
                maxDate = e.visibleDates[e.visibleDates.Count - 1].Date;

                //// We have already used DateTime of MinValue and MaxValue as threshold value
                //// in Source , so that set hardcoded values
                schedule.MinDisplayDate = new DateTime(01, 01, 01);
                schedule.MaxDisplayDate = new DateTime(9999, 12, 31);
            }
        }
        private void Week_Clicked(object sender, EventArgs e)
        {
            schedule.ScheduleView = ScheduleView.WeekView;
        }
        private void Day_Clicked(object sender, EventArgs e)
        {
            schedule.ScheduleView = ScheduleView.DayView;
            schedule.MinDisplayDate = (DateTime)minDate;
            schedule.MaxDisplayDate = (DateTime)maxDate;
        }
    }
}
