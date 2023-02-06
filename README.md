# How to show a particular week in a day view of Xamarin Schedule (SfSchedule)

You can restrict the day view for the selected week only by using the [MinDisplayDate](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfSchedule.XForms.SfSchedule.html#Syncfusion_SfSchedule_XForms_SfSchedule_MinDisplayDate) and [MaxDisplayDate](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfSchedule.XForms.SfSchedule.html#Syncfusion_SfSchedule_XForms_SfSchedule_MaxDisplayDate) properties of Xamarin [SfSchedule](https://www.syncfusion.com/xamarin-ui-controls/xamarin-scheduler).

**XAML**

Set the [FirstDayOfWeek](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfSchedule.XForms.SfSchedule.html#Syncfusion_SfSchedule_XForms_SfSchedule_FirstDayOfWeek) as 2 and [ScheduleView](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfSchedule.XForms.SfSchedule.html#Syncfusion_SfSchedule_XForms_SfSchedule_ScheduleView) as `WeekView`.
```
<ContentPage.ToolbarItems>
        <ToolbarItem x:Name="Day" Priority="1" Order="Primary" Text="Day"/>
        <ToolbarItem x:Name="Week" Priority="1" Order="Primary" Text="Week"/>
    </ContentPage.ToolbarItems>
    <ContentPage.Behaviors>
        <local:SchedulerBehavior/>
    </ContentPage.Behaviors>
    <ContentPage.Content>
        <syncfusion:SfSchedule x:Name="schedule" FirstDayOfWeek="2" ScheduleView="WeekView" DataSource="{Binding Appointments}">
            <syncfusion:SfSchedule.BindingContext>
                <local:SchedulerViewModel/>
            </syncfusion:SfSchedule.BindingContext>
        </syncfusion:SfSchedule>
    </ContentPage.Content>
```
**C#**

You can get the week’s first and last date by using the `VisibleDatesChangedEventArgs` argument of `VisibleDatesChanged` event, using this you can set the `MinDisplayDate` and `MaxDisplayDate` for schedule while navigating to day view. Also, changing the min/max value while navigating back to week view.
```
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
```

KB article - [How to show a particular week in a day view of Xamarin Schedule (SfSchedule)](https://www.syncfusion.com/kb/12251/how-to-show-a-particular-week-in-a-day-view-of-xamarin-schedule-sfschedule)