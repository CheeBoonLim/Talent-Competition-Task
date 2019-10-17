import React from 'react';
import Cookies from 'js-cookie';

export const CloseJob = (id, status) => {
    var cookies = Cookies.get('talentAuthToken');
    var link = 'http://localhost:51689/listing/listing/closeJob';
    if (status == 0) {
        $.ajax({
            url: link,
            headers: {
                'Authorization': 'Bearer ' + cookies,
                'Content-Type': 'application/json'
            },
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(id),
            success: function (res) {
                if (res.success == true) {
                    TalentUtil.notification.show(res.message, "success", null, null);
                    window.location = "/ManageJobs";
                } else {
                    TalentUtil.notification.show(res.message, "error", null, null)
                }
            }.bind(this)
        })
    } else {
        TalentUtil.notification.show("Job already closed.", "error", null, null);
    }
}