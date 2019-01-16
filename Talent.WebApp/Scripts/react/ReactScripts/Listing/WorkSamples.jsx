/* Work Samples section */
import React from 'react';
export class WorkSamples extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="four wide column">
                    <h3>Work Samples</h3>
                    <div className="tooltip">Upload samples of your work here</div>
                </div>
                <div className="twelve wide column">
                    <section>
                        <div>
                            <label htmlFor="work_sample_uploader" className="work-sample-photo">
                                <i className="huge circular camera retro icon"></i>
                            </label>
                        </div>
                    </section>
                </div>
            </div>
        )
    }
}