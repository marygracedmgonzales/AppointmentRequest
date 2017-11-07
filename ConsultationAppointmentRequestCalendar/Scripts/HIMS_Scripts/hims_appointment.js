$(document).ready(function () {
    $(function () {
        $(".datepicker").datepicker({
            format: 'mm-dd-yyyy',
            startDate: new Date(),
            autoclose: true,
        });

        $(".datepickerFilter").datepicker({
            format: 'mm-dd-yyyy'
        });
    });
    $(".datepicker").attr("readOnly", true);
    initFunctions();
    initAjaxDropDown();
    initBindings();
});

var physicianId = 0;
var patientList = null;
var scheduledPatientId = 0;
var scheduledPatientPatientDetails = null;
var appointmentSchedule = null;
var appointmentList = null;
var scheduledPatientAppointment = null;
var appointmentTimeId = 0;
var scheduleId = 0;
var saveOrUpdate = "save";

function initBindings() {
    appointmentBtnEdit();
    appointmentBtnCancel();
    appointmentBtnDelete();
    appointmentBtnSave();
    filterAppointments()
}

function initFunctions() {
    $("#appointmentPhysician").on('change', function () {
        var physicianId = $("#appointmentPhysician").val();
        hideButtons();
        $("#physician-schedule-noavailable").removeClass("hidden");
        $("#physician-time-noavailable").removeClass("hidden");
        $("#selectedDate").prop("disabled", true);
        $("#physician-schedule-table").empty();
        $("#appointmentSpecialization").text("");
        $("#appointmentPhysicianName").text("");
        $("#scheduledTimeTable").empty();
        $("#selectedDate").val("");
        clearAppointmentData();
        if (physicianId != 0) {
            ajaxGetPhysician(physicianId);
        }
    });

    $("#appointmentPhysician").on('blur', function () {
        $("#appointmentPhysician").val(0);
    });
    
    $("#selectedDate").on('changeDate', function () {
        hideButtons();
        clearAppointmentData();
        var selectedDate = $("#selectedDate").val();
        ajaxGetSchedule(selectedDate, physicianId);
    });

    $("#appointmentPatient").on('blur', function () {
        $("#appointmentPatient").val(0);
    });
    
    $("#appointmentPatient").on('change', function () {
        var patientId = $("#appointmentPatient").val();
        $("#appointmentPurpose").val(1);
        $("#appointmentRemarks").val("");
        if (patientId != 0) {
            var patient = $.grep(patientList, function (e) { return e.PatientId == patientId; });
            var time = $.grep(appointmentSchedule.schedule.Time, function (e) { return e.TimeId == appointmentTimeId; });
            var from = formatAppointmentTime(time[0].StartTime);
            var to = formatAppointmentTime(time[0].EndTime);
            $("#appointmentPatientId").text(patient[0].PatientId);
            $("#appointmentFName").text(patient[0].FirstName);
            $("#appointmentMname").text(patient[0].MiddleName);
            $("#appointmentLName").text(patient[0].LastName);
            $("#appointmentAge").text(patient[0].Age);
            $("#appointmentDate").text($("#selectedDate").val());
            $("#appointmentDay").text(appointmentSchedule.schedule.Day);
            $("#appointmentTime").text(from + ' - ' + to);
        }
    });
}

//AJAX call
function initAjaxDropDown() {
    $.ajax({
        type: "GET",
        url: "/Appointment/GetPatientList",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            patientList = data;
            populatePatienListDropdown();
        },
        error: function (e) {
            console.log("Error retrieving patient list.");
        }
    });

    ajaxGetAppointmentList();
}

function ajaxGetAppointmentList() {
    $.ajax({
        type: "GET",
        url: "/Appointment/GetAppointmentList",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            appointmentList = data;
        },
        error: function (e) {
            console.log("Error retrieving patient list.");
        }
    });
}

function ajaxGetPhysician(physicianId) {
    $.ajax({
        type: "GET",
        url: "/Appointment/GetPhysician?PhysicianId=" + physicianId,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#physician-schedule-table").empty();
            templateGetPhysician(data);
        },
        error: function () {
            $("#scheduledTimeTable").empty();
            $("#scheduledTimeTable").append('<span>Error occurred while retrieving physician appointment record(s). Please try again!</span>');
        }
    });
}

function ajaxGetSchedule(selectedDate, physicianId) {
    $.ajax({
        type: "GET",
        url: "/Appointment/GetSchedule?PhysicianId=" + physicianId + "&SelectedDate=" + selectedDate,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            appointmentSchedule = data;
            templateGetSchedule(data);
        },
        error: function () {
            $("#scheduledTimeTable").empty();
            $("#scheduledTimeTable").append('<span>Error occurred while generating schedule. Please try again!</span>');
        }
    });
}

function ajaxSaveAppointmentRequest(newRequest) {
    $.ajax({
        type: "POST",
        url: "/Appointment/SaveAppointment",
        async: false,
        data: JSON.stringify(newRequest),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                errorNotification("success", "saving appointment request");
            }
        },
        error: function () {
            errorNotification("error", "saving appointment request");
        }
    });
}

function ajaxUpdateAppointmentRequest(updatedRequest) {
    $.ajax({
        type: "POST",
        url: "/Appointment/UpdateAppointment",
        async: false,
        data: JSON.stringify(updatedRequest),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                errorNotification("success", "updating appointment request");
            }
        },
        error: function () {
            errorNotification("error", "updating appointment request");
        }
    });
}

function ajaxDeleteAppointmentRequest(toDeleteId) {
    $.ajax({
        type: "GET",
        url: "/Appointment/DeleteAppointment?toDeleteId=" + toDeleteId,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                errorNotification("success", "deleting appointment request");
            }
        },
        error: function () {
            errorNotification("error", "deleting appointment request");
        }
    });
}

function ajaxFilterByIdAndDateAppointmentList(filteredPhysicianId, filteredDate){
    alert("ajaxFilterByIdAndDateAppointmentList");

    $.ajax({
        type: "GET",
        url: "/Appointment/ViewPhysicianAppointmentList?toDeleteId=" + toDeleteId,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            $('partialviewselector').html(data);
            if (data != null) { 
                errorNotification("success", "deleting appointment request");
            }
        },
        error: function () {
            errorNotification("error", "deleting appointment request");
        }
    });
}

function ajaxFilterByIdAppointmentList(filteredPhysicianId) {
    alert("ajaxFilterByIdAppointmentList");

    $.ajax({
        type: "GET",
        url: "/Appointment/ViewPhysicianAppointmentList?PhysicianId=" + filteredPhysicianId,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('ViewPhysicianAppointmentList').html(data);
        },
        error: function () {
            errorNotification("error", "retrieving appointment list");
        }
    });
    alert("ajaxFilterByIdAppointmentList---done");
}

function ajaxFilterByDateAppointmentList(filteredDate) {
    alert("ajaxFilterByDateAppointmentList");
}

//HTML Templates
function templateGetSchedule(data) {
    $("#scheduledTimeTable").empty();
    $("#physician-time-noavailable").removeClass("hidden");
    if (data.schedule != null) {
        scheduledTime = data.schedule;
        if (data.schedule.Time != null) {
            $("#physician-time-noavailable").addClass("hidden");
            $.each(data.schedule.Time, function (index, time) {
                var from = formatAppointmentTime(time.StartTime);
                var to = formatAppointmentTime(time.EndTime);
                var resultBtn = '<button type="button" data-id="' + time.TimeId + '" class="btn btn-default btn-appointment setAppointment">Set Appointment</button>';
                
                $.each(data.appointments, function (i, appointment) {
                    var appointmentDate = moment(appointment.Date).format('MM-DD-YYYY');
                    if (time.TimeId == appointment.TimeId && appointmentDate == $("#selectedDate").val()) {
                        resultBtn = '<button type="button" data-id="' + appointment.AppointmentId + '" class="btn btn-default btn-appointment previewAppointment">Preview</button>';
                    }
                });

                var timeTemplate = '<tr>'
                                 + '<td>' + from + '</td>'
                                 + '<td>' + ' - ' + '</td>'
                                 + '<td>' + to + '</td>'
                                 + '<td>' + resultBtn + '</td>'
                                 + '</tr>';
                $("#scheduledTimeTable").append(timeTemplate);
            })
            setAppointment();
            previewAppointment();
        }
    }    
}

function templateGetPhysician(data) {
    physicianId = data.PhysicianId;
    $("#appointmentSpecialization").text(data.Specialization);
    $("#appointmentPhysicianName").text("Doctor " + data.LastName + ", " + data.FirstName + " " + data.MiddleName);

    if (data.Schedule != null) {
        $("#physician-schedule-noavailable").addClass("hidden");
        $("#selectedDate").prop("disabled", false);
        $.each(data.Schedule, function (index, sched) {
            var from = formatAppointmentTime(sched.Time[0].StartTime);
            var to = formatAppointmentTime(sched.Time[sched.Time.length - 1].EndTime);
            var scheduleTemplate = '<tr>'
                             + '<td>' + sched.Day + '  </td>'
                             + '<td>' + from + '</td>'
                             + '<td>' + ' - ' + '</td>'
                             + '<td>' + to + '</td>'
                             + '</tr>';

            $("#physician-schedule-table").append(scheduleTemplate);
        });
    } else {
        $("#physician-schedule-noavailable").removeClass("hidden");
        $("#physician-time-noavailable").removeClass("hidden");
        $("#selectedDate").prop("disabled", true);
    }
}

//Functions
function setAppointment() {
    $(".setAppointment").on('click', function () {
        hideButtons();
        appointmentTimeId = $(this).attr("data-id");
        clearAppointmentFieldRequest();
        $("#appointmentBtnSave").removeClass("hidden");
        saveOrUpdate = "save";
    });
}

function previewAppointment() {
    $(".previewAppointment").on('click', function () {
        hideButtons();
        clearAppointmentData();
        $("#appointmentPatient").addClass("hidden");
        var dataId = $(this).attr("data-id");
        scheduledPatientAppointment = $.grep(appointmentSchedule.appointments, function (e) { return e.AppointmentId == dataId; });
        scheduledPatientDetails = $.grep(patientList, function (e) { return e.PatientId == scheduledPatientAppointment[0].PatientId; });
        setAppointmentPatientValueEdit();

        $("#appointmentBtnSave").data("id", dataId);
        $("#appointmentBtnEdit").data("id", dataId);
        $("#appointmentBtnCancel").data("id", dataId);
        $("#appointmentBtnDelete").data("id", dataId);

        $("#appointmentBtnEdit").removeClass("hidden");
        $("#appointmentBtnDelete").removeClass("hidden");
    });
}

function appointmentBtnEdit() {
    $("#appointmentBtnEdit").on('click', function () {
        $("#appointmentPatient").removeClass("hidden");
        $("#appointmentBtnCancel").removeClass("hidden");
        $("#appointmentBtnSave").removeClass("hidden");
        saveOrUpdate = "update";
        $("#appointmentBtnEdit").addClass("hidden");
        enableDisableProp(false);
    });
}

function appointmentBtnCancel() {
    $("#appointmentBtnCancel").on('click', function () {
        $("#appointmentBtnEdit").removeClass("hidden");
        $("#appointmentBtnSave").addClass("hidden");
        $("#appointmentBtnCancel").addClass("hidden");
        setAppointmentPatientValueEdit();
        enableDisableProp(true);
    });
}

function appointmentBtnDelete() {
    $("#appointmentBtnDelete").on('click', function () {
        var toDeleteId = $("#appointmentId").text();
        ajaxDeleteAppointmentRequest(toDeleteId);
        refreshAppointmentList();
    });
}

function appointmentBtnSave() {
    $("#appointmentBtnSave").on('click', function () {

        if (saveOrUpdate == "save") {
            var newAppointmentObject = {
                "PatientId": $("#appointmentPatientId").text(),
                "PhysicianId": physicianId,
                "ScheduleId": scheduledTime.ScheduleId,
                "Date": $("#selectedDate").val(),
                "TimeId": appointmentTimeId,
                "Purpose": $("#appointmentPurpose").val(),
                "Remarks": $("#appointmentRemarks").val()
            };
            ajaxSaveAppointmentRequest(newAppointmentObject);
        } else {
            var updateAppointmentObject = {
                "AppointmentId": scheduledPatientAppointment[0].AppointmentId,
                "PatientId": $("#appointmentPatientId").text(),
                "PhysicianId": scheduledPatientAppointment[0].PhysicianId,
                "ScheduleId": scheduledPatientAppointment[0].ScheduleId,
                "Date": $("#selectedDate").val(),
                "TimeId": scheduledPatientAppointment[0].TimeId,
                "Purpose": $("#appointmentPurpose").val(),
                "Remarks": $("#appointmentRemarks").val()
            };
            ajaxUpdateAppointmentRequest(updateAppointmentObject);
        }
        refreshAppointmentList();
    });
}


function setAppointmentPatientValueEdit() {
    $("#appointmentId").text(scheduledPatientAppointment[0].AppointmentId);
    $("#appointmentPatientId").text(scheduledPatientDetails[0].PatientId);
    $("#appointmentFName").text(scheduledPatientDetails[0].FirstName);
    $("#appointmentMname").text(scheduledPatientDetails[0].MiddleName);
    $("#appointmentLName").text(scheduledPatientDetails[0].LastName);
    $("#appointmentAge").text(scheduledPatientDetails[0].Age);

    var scheduledTimeDetails = $.grep(appointmentSchedule.schedule.Time, function (e) { return e.TimeId == scheduledPatientAppointment[0].TimeId; });
    var from = formatAppointmentTime(scheduledTimeDetails[0].StartTime);
    var to = formatAppointmentTime(scheduledTimeDetails[0].EndTime);
    $("#appointmentDate").text(moment(scheduledPatientAppointment[0].Date).format('LL'));
    $("#appointmentDay").text(appointmentSchedule.schedule.Day);
    $("#appointmentTime").text(from +' - '+ to);
    $("#appointmentPurpose").val(scheduledPatientAppointment[0].Purpose);
    $("#appointmentRemarks").val(scheduledPatientAppointment[0].Remarks);
}

function populatePatienListDropdown() {
    $.each(patientList, function (index, patient) {
       var patientTemplate = '<option value="' + patient.PatientId + '">' + patient.LastName + ', ' + patient.FirstName + ' ' + patient.MiddleName + '</option>';
       $("#appointmentPatient").append(patientTemplate);
    });
}

function clearAppointmentFieldRequest() {
    $("#appointmentPatient").removeClass("hidden");
    $("#appointmentPatient").val(0);
    $("#appointmentPatientId").text("");
    $("#appointmentFName").text("");
    $("#appointmentMname").text("");
    $("#appointmentLName").text("");
    $("#appointmentAge").text("");
    $("#appointmentDate").text("");
    $("#appointmentDay").text("");
    $("#appointmentTime").text("");
    $("#appointmentPurpose").val(1);
    $("#appointmentRemarks").val("");
    enableDisableProp(false);
}

function clearAppointmentData() {
    $("#appointmentId").text("");
    $("#appointmentPatient").val(0);
    $("#appointmentPatientId").text("");
    $("#appointmentFName").text("");
    $("#appointmentMname").text("");
    $("#appointmentLName").text("");
    $("#appointmentAge").text("");
    $("#appointmentDate").text("");
    $("#appointmentDay").text("");
    $("#appointmentTime").text("");
    $("#appointmentPurpose").val(1);
    $("#appointmentRemarks").val("");
    enableDisableProp(true);
}

function enableDisableProp(value) {
    $("#appointmentPatient").prop("disabled", value);
    $("#appointmentPurpose").prop("disabled", value);
    $("#appointmentRemarks").prop("disabled", value);
}

function hideButtons() {
    $("#appointmentBtnSave").addClass("hidden");
    $("#appointmentBtnEdit").addClass("hidden");
    $("#appointmentBtnCancel").addClass("hidden");
    $("#appointmentBtnDelete").addClass("hidden");
}

function formatAppointmentTime(time) {
    var formattedTime = time < 12 ? time + ":00AM" : (time) + ":00PM";
    return formattedTime;
}

function errorNotification(type, message) {
    $("#alertDiv").empty();
    if (type == "error") {
        $("#alertDiv").append('<div class="alert alert-danger hidden"><strong>Error!</strong> '+message+'.</div>');
    } else {
        $("#alertDiv").append('<div class="alert alert-success"><strong>Success!</strong> ' + message + '.</div>');
    }
    setTimeout(function () {
        $("#alertDiv").empty();
    }, 3000);
}

function refreshAppointmentList(){
    hideButtons();
    clearAppointmentData();
    var selectedDate = $("#selectedDate").val();
    ajaxGetSchedule(selectedDate, physicianId);
}

function filterAppointments() {
    $("#filterAppointmentPhysician").on('change', function () {
        $("#filterAppointmentBtn").prop("disabled", false);
    });

    $("#filterAppointmentBtn").on('click', function () {
        var filterId = $("#filterAppointmentPhysician").val();
        if (filterId != 0) {
            $("#filterAppointmentResetBtn").prop("disabled", false);
            $("#appointment-list-all").empty();
            $("#filtered-result-appointment").load('/Appointment/ViewPhysicianAppointmentList?PhysicianId=' + filterId);
        }
    });

    $("#filterAppointmentResetBtn").on('click', function () {
        location.reload();
    });
}