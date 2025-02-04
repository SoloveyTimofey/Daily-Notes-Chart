using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.ChartCluster.ValueObjects;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.NoteCluster.ValueObjects;
using DailyNotesChart.Domain.Shared;

namespace DailyNotesChart.Domain.Errors;

public static class DomainErrors
{
    public static class ChartGroup
    {
        public static readonly Error InvalidChartGroupName = new Error(
            "ChartGroup.InvalidName",
            $"Invalid chart group name. Name lenght must be between {ChartGroupName.NAME_MIN_LENGHT} and {ChartGroupName.NAME_MAX_LENGHT}.");

        public static readonly Error CannotAddChartWithExistingDateInChartGroup = new Error(
            "ChartGroup.CannotAddChartWithExistingDateInChartGroup",
            $"Cannot add chart. This chart group already has a chart with the same date.");

        public static readonly Error NoteTemplateAlreadyAdded = new Error(
            "ChartGroup.NoteTemplateAlreadyAdded",
            $"Cannot add note template. This chart already has note template. Try change note template instead.");
    }

    public static class Chart
    {
        public static readonly Error InvaildChartDate = new Error(
            "Chart.InvalidDate",
            $"Inalid chart date. Date must be between {ChartDate.MIN_DATE} and {ChartDate.MAX_DATE}.");

        public static readonly Error InvalidChartSummary = new Error(
            "Chart.InvalidSummary",
            $"Invalid chart summary. Summary lenght must be between {ChartSummary.SUMMARY_MIN_LENGHT} and {ChartSummary.SUMMARY_MIN_LENGHT}.");

        public static readonly Error StartValueGreaterThanEndValue = new Error(
            "Chart.StartValueGreaterThanEndValue",
            $"Invalid start and end values. Start value cannot be greater than end valie.");

        public static readonly Error ValuesOutOfRange = new Error(
            "Chart.ValuesOutOfRange",
            $"Start or end values are out of range. Start value cannot be greater than {YAxeValues.MIN_VALUE} and end value cannot be greater than {YAxeValues.MAX_VALUE}.");

        public static readonly Error InvalidYAxeName = new Error(
            "Chart.InvalidYAxeName",
            $"Invalid y axe name. Name lenght of y axe must be between {YAxeName.NAME_MIN_LENGHT} and {YAxeName.NAME_MAX_LENGHT}.");

        public static readonly Error CannotAddNoteWithDuplicateCoordinates = new Error(
            "Chart.CannotAddNoteWithDuplicateCoordinates",
            "Cannot add note. Specified chart already contains note with specified coordinates.");
    }

    public static class Note
    {
        //public static readonly Error SpecifiedYAxeValueOutOfRange = new Error(
        //    "Note.SpecifiedYAxeValueOutOfRange",
        //    "Cannot create note. Specified Y axe value is out of chart's range.");
    }

    public static class NoteTemplate
    {
        public static readonly Error InvalidColorFormat = new Error(
            "Color.InvalidColorFormat",
            "Invalid color format. Expected #RRGGBB.");

        public static readonly Error InvalidDescription = new Error(
            "NoteTemplate.InvalidDescription",
            $"Invalid note template description. Description lenght must be between {NoteDescription.DESCRIPTION_MIN_LENGHT} and {NoteDescription.DESCRIPTION_MAX_LENGHT}");
    }
}