/* Description section */
import React from 'react';

export class Description extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Description</h3>
                    <div className="tooltip">Write a description to describe the service you provide.</div>
                </div>
                <div className="ten wide column">
                    <div className="field">
                        <textarea placeholder="Write a description to describe the service you provide."></textarea>
                    </div>
                    <p>Description must be between 150-600 characters.</p>
                </div>
            </React.Fragment>
        )
    }
}
