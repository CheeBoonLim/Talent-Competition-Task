import React from 'react';
import { CreateJob } from '../CreateJob/CreateJob.jsx';

export const JobList = (lists, closeJob) => {
    if (lists.length == 0) {
        return (
            <h4 style={{ fontWeight: "normal" }}>No Jobs Found</h4>
        )
    } else {
        return (
            <div className="ui three cards">
                {lists.map((job) =>
                    <div className="ui card" key={job.id}>
                        <div className="content">
                            <div className="header">{job.title}</div>
                            <a className="ui black right ribbon label">
                                <i aria-hidden="true" className="user icon"></i>
                                0
                            </a>
                            <div className="meta">{job.location.city}, {job.location.country}</div>
                            <div className="description" style={{ minHeight: "150px" }}>{job.summary}</div>
                        </div>
                        <div className="extra content">
                            <div className="ui left floated buttons">
                                {(job.status == 0) ? (
                                    <button className="ui green button" style={{ fontSize: "0.78571429rem" }}>Active</button>
                                ) : (
                                    <button className="ui red button" style={{ fontSize: "0.78571429rem" }}>Expired</button>
                                )}
                            </div>
                            <div className="ui right floated buttons">
                                <button className="ui blue basic button" style={{ fontSize: "0.78571429rem" }} onClick={() => closeJob(job.id, job.status)}><i aria-hidden="true" className="ban icon"></i>Close</button>
                                <button className="ui blue basic button" style={{ fontSize: "0.78571429rem" }} onClick={() => window.location = "/EditJob/" + job.id}><i aria-hidden="true" className="edit icon"></i>Edit</button>
                                <button className="ui blue basic button" style={{ fontSize: "0.78571429rem" }}><i aria-hidden="true" className="copy outline icon"></i>Copy</button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        )
    }
}