
function DeleteCalendarEvent(actionUrl, CalendarEventName, CalendarEventDescription, CalendarEventDate,
    CalendarEventStartTime, CalendarEventEndTime, AssignedToUser)
{
    Swal.fire({
        title: 'Confirm Deletion',
        html: `
            <h4 style="color: red;">Are you sure you want to delete the Calendar Event?</h4>
            <div>
            <hr class="horizontal-line" style="height: 3px; border: 1px solid gray; margin: 10px">
            <h2>Calendar Event Details:</h2>
            <strong>Name:</strong> ${CalendarEventName}<br>
            <strong>Description:</strong> ${CalendarEventDescription}<br>
            <strong>Date:</strong> ${CalendarEventDate}<br>
            <strong>Start Time:</strong> ${CalendarEventStartTime}<br>
            <strong>End Time:</strong> ${CalendarEventEndTime}<br>
            <strong>Assigned To User:</strong> ${AssignedToUser}
            </div>
        `,       
        icon: 'question',
        showCancelButton: true
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: actionUrl,
                type: 'POST',
                success: function (result) {
                    localStorage.setItem('calendarEventDeletedMessage', 'Your Calendar Event has been deleted successfully');
                    location.reload();
                },
                error: function (error) {
                    alert("failure");
                }
            });

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire('Cancelled', 'Your Calendar Event is safe :)', 'info');
        }
    });
}

function DeleteWorkMode(actionUrl, WorkModeName, UserName, UserSurname, DateOfWorkmode)
{
    console.log(actionUrl)
    console.log(WorkModeName)
    console.log(UserName)
    console.log(UserSurname)
    console.log(DateOfWorkmode)
    Swal.fire({
        title: 'Confirm Deletion',
        html: `
            <h4 style="color: red;">Are you sure you want to delete the work mode?</h4>
            <div>
            <hr class="horizontal-line" style="height: 3px; border: 1px solid gray; margin: 10px">
            <h2>Work Mode Details:</h2>
            <strong>Work Mode:</strong> ${WorkModeName}<br>
            <strong>Assigned to User:</strong> ${UserName} ${UserSurname}<br>
            <strong>Date:</strong> ${DateOfWorkmode}<br>
            </div>
        `,
        icon: 'question',
        showCancelButton: true
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: actionUrl,
                type: 'POST',
                success: function (result) {
                    localStorage.setItem('workModeDeletedMessage', 'Your Work Mode has been deleted successfully');
                    location.reload();
                },
                error: function (error) {
                    alert("failure");
                }
            });

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire('Cancelled', 'Your Work Mode is safe :)', 'info');
        }
    });
}
function DeleteUser(actionUrl, UserName, UserSurname, UserEmail,
    UserRoles) {
    Swal.fire({
        title: 'Confirm Deletion',
        html: `
            <h4 style="color: red;">Are you sure you want to delete the User?</h4>
            <div>
            <hr class="horizontal-line" style="height: 3px; border: 1px solid gray; margin: 10px">
            <h2>User Details:</h2>
            <strong>Name:</strong> ${UserName}<br>
            <strong>Surname:</strong> ${UserSurname}<br>
            <strong>Email:</strong> ${UserEmail}<br>
            <strong>User Role:</strong> ${UserRoles}<br>
            </div>
        `,
        icon: 'question',
        showCancelButton: true
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: actionUrl,
                type: 'POST',
                success: function (result) {
                    localStorage.setItem('userDeletedMessage', 'Chosen User has been deleted successfully');
                    location.reload();
                },
                error: function (error) {
                    alert("failure");
                }
            });

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire('Cancelled', 'User is safe :)', 'info');
        }
    });
}

function CreateCalendarEvent(actionUrl, initialName, initialDescription, initialDate, initialStartTime, initialEndTime, assignedToUser) {
    Swal.fire({
        title: 'Create New Calendar Event',
        html: `
            <form id="createEventForm">
                <div class="form-group">
                    <label for="NewCalendarEventName">Name:</label>
                    <input type="text" class="form-control" id="NewCalendarEventName" name="NewCalendarEventName" value="${initialName}" required>
                </div>
                <div class="form-group">
                    <label for="NewCalendarEventDescription">Description:</label>
                    <input type="text" class="form-control" id="NewCalendarEventDescription" name="NewCalendarEventDescription" value="${initialDescription}">
                </div>
                <div class="form-group">
                    <label for="NewCalendarEventDate">Date:</label>
                    <input type="date" class="form-control" id="NewCalendarEventDate" name="NewCalendarEventDate" value="${initialDate}" required>
                </div>
                <div class="form-group">
                    <label for="NewCalendarEventStartTime">Start Time:</label>
                    <input type="time" class="form-control" id="NewCalendarEventStartTime" name="NewCalendarEventStartTime" value="${initialStartTime}" required>
                </div>
                <div class="form-group">
                    <label for="NewCalendarEventEndTime">End Time:</label>
                    <input type="time" class="form-control" id="NewCalendarEventEndTime" name="NewCalendarEventEndTime" value="${initialEndTime}" required>
                </div>
                <div class="form-group">
                    <label for="AssignedToUser">Assigned To User:</label>
                    <input type="text" class="form-control" id="AssignedToUser" name="AssignedToUser" value="${assignedToUser}" required>
                </div>
            </form>
        `,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Create',
        preConfirm: () => {
            return {
                NewCalendarEventName: document.getElementById('NewCalendarEventName').value,
                NewCalendarEventDescription: document.getElementById('NewCalendarEventDescription').value,
                NewCalendarEventDate: document.getElementById('NewCalendarEventDate').value,
                NewCalendarEventStartTime: document.getElementById('NewCalendarEventStartTime').value,
                NewCalendarEventEndTime: document.getElementById('NewCalendarEventEndTime').value,
                AssignedToUser: document.getElementById('AssignedToUser').value
            };
        }
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: result.value,
                success: function (result) {
                    localStorage.setItem('newCalendarEventCreatedMessage', 'Your new Calendar Event has been created successfully');
                    location.reload();
                },
                error: function (error) {
                    alert("Failure");
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire('Cancelled', 'Your new Calendar Event creation is cancelled :)', 'info');
        }
    });
}
