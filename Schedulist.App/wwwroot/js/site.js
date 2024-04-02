
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
            <strong>StartTime:</strong> ${CalendarEventStartTime}<br>
            <strong>EventEndTime:</strong> ${CalendarEventEndTime}<br>
            <strong>AssignedToUser:</strong> ${AssignedToUser}
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

function DeleteWorkMode(actionUrl, WorkModeToUserID, WorkModeName, UserID, DateOfWorkmode)
{
    Swal.fire({
        title: 'Confirm Deletion',
        html: `
            <h4 style="color: red;">Are you sure you want to delete the work mode?</h4>
            <div>
            <hr class="horizontal-line" style="height: 3px; border: 1px solid gray; margin: 10px">
            <h2>Work Mode Details:</h2>
            <strong>WorkModeToUserID:</strong> ${WorkModeToUserID}<br>
            <strong>WorkModeName:</strong> ${WorkModeName}<br>
            <strong>UserID:</strong> ${UserID}<br>
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
