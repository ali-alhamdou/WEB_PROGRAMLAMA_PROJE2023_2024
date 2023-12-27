// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function FillDoctors(lstDepartmentCtrl, lstDoct) {
    var lstDoctors = $("#" + lstDoct);
    lstDoctors.empty();

    var selectedDepartment = lstDepartmentCtrl.options[lstDepartmentCtrl.selectedIndex].value;

    if (selectedDepartment != null && selectedDepartment != '') {
        $.getJSON("/reservation/GetDoctorsByDepartment", { deptID: selectedDepartment }, function (doctors) {
            if (doctors != null && !jQuery.isEmptyObject(doctors)) {
                $.each(doctors, function (index, doctor) {
                    lstDoctors.append($('<option/>', {
                        value: doctor.value,
                        text: doctor.text
                    }));
                });
            };
        });
    }
    return;
}