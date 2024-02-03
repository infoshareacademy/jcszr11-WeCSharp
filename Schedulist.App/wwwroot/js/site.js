
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

function DeleteWorkMode(actionUrl, WorkModeToUserID, WorkModeName, UserID, DateOfWorkmode)
{
    console.log(actionUrl)
    console.log(WorkModeToUserID)
    console.log(WorkModeName)
    console.log(UserID)
    console.log(DateOfWorkmode)
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
