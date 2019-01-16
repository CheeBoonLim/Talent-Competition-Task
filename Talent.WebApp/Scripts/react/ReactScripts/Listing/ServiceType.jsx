/* ServiceType section */
import React from 'react';

export class ServiceType extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Service Type</h3>
                    <div className="tooltip">
                        Please select between an hourly basis service or a One-off service
                    </div>
                </div>
                <div className="ten wide column">
                    <div className="inline fields">
                        <div className="field">
                            <div className="ui radio checkbox">
                                <input name="serviceType" tabIndex="0" className="hidden" type="radio"></input>
                                <label>Hourly basis service</label>
                            </div>
                        </div>
                        <div className="field">
                            <div className="ui radio checkbox">
                                <input name="serviceType" tabIndex="0" className="hidden" type="radio"></input>
                                <label>One-off service</label>
                            </div>
                        </div>
                    </div>
                    <div className="tooltip">
                        One-off services is a one time service charge, this also includes the sale of products.<br />
                        An on-going service describes a fee per hour basis for the service provided
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
