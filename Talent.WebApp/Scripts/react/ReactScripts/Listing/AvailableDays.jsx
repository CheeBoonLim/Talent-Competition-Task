/* AvailableDays section */
import React from 'react';

export class AvailableDays extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Available days</h3>
                    <div className="tooltip">
                        Please select your available start and end dates. Please also specify start and end times.
                    </div>
                </div>
                <div className="ten wide column">
                    <div className="form-wrapper">
                        <div className="fields">
                            <div className="two wide field">
                                <label>Start date</label>
                            </div>
                            <div className="five wide field">
                                <input name="startDate" placeholder="Start date" type="text"></input>
                            </div>
                            <div className="two wide field"><label>End date (optional)</label></div>
                            <div className="five wide field">
                                <input name="endDate" placeholder="End date" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" checked="" type="checkbox"></input>
                                    <label>Sun</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" checked="" type="checkbox"></input>
                                    <label>Mon</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" type="checkbox"></input>
                                    <label>Tues</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" type="checkbox"></input>
                                    <label>Wed</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" checked="" type="checkbox"></input>
                                    <label>Thurs</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" type="checkbox"></input>
                                    <label>Fri</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>

                        <div className="fields">
                            <div className="two wide field">
                                <div className="ui checkbox">
                                    <input tabIndex="0" className="hidden" type="checkbox"></input>
                                    <label>Sat</label>
                                </div>
                            </div>
                            <div className="four wide field">
                                <input name="startTime" placeholder="Start time" type="text"></input>
                            </div>
                            <div className="four wide field">
                                <input name="endTime" placeholder="End time" type="text"></input>
                            </div>
                        </div>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
