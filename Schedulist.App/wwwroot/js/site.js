
function DeleteCalendarEvent(actionUrl, CalendarEventName, CalendarEventDescription, CalendarEventDate,
    CalendarEventStartTime, CalendarEventEndTime, AssignedToUser)
{
    console.log(actionUrl)
    Swal.fire({
        title: 'Confirm Deletion',
        html: `
            <h4 style="color: red;">Are you sure you want to delete the calendar event?</h4>
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
                    localStorage.setItem('calendarEventDeletedMessage', 'Your calendar event has been deleted successfully');
                    location.reload();
                },
                error: function (error) {
                    alert("failure");
                }
            });

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire('Cancelled', 'Your calendar event is safe :)', 'info');
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
