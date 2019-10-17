import React from 'react';
import Cookies from 'js-cookie';
import { Popup } from 'semantic-ui-react';
import moment from 'moment';
import { JobList } from './JobList.jsx';
import { CloseJob } from './CloseJob.jsx';
import { Pagination } from './Pagination.jsx';

export class JobSummaryCard extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            jobs: []
        };

        this.loadData = this.loadData.bind(this)
        this.updateWithoutSave = this.updateWithoutSave.bind(this)
    }

    componentDidMount() {
        this.loadData();
    };

    loadData() {
        var link = 'http://localhost:51689/listing/listing/getEmployerJobs';
        var cookies = Cookies.get('talentAuthToken');
        $.ajax({
            url: link,
            headers: {
                'Authorization': 'Bearer ' + cookies,
                'Content-Type': 'application/json'
            },
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (res) {
                let loadJobs = [];
                if (res.myJobs) {
                    loadJobs = res.myJobs
                    //console.log("loadJobs", loadJobs)
                }
                this.updateWithoutSave(loadJobs)
            }.bind(this),
            error: function (res) {
                console.log(res.status)
            }
        })
    }

    updateWithoutSave(loadJobs) {
        let newSD = Object.assign([], this.state.jobs, loadJobs)
        this.setState({
            jobs: newSD
        })
    }

    render() {
        return (
            <div className="ui container">
                <h1>List of Jobs</h1>
                <h4><i aria-hidden="true" className="filter icon"></i><span style={{ fontWeight: "normal" }}>Filter: </span>Choose filter<i aria-hidden="true" className="caret down icon"></i><i aria-hidden="true" className="calendar alternate icon"></i><span style={{ fontWeight: "normal" }}>Sort by date: </span>Newest first<i aria-hidden="true" className="caret down icon"></i></h4>
                {JobList(this.state.jobs, CloseJob)}
                <Pagination />
            </div>
        )
    }
}