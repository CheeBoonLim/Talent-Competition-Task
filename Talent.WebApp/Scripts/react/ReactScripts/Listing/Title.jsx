/* Title section */
import React from 'react';

export class Title extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Title</h3>
                    <div className="tooltip">Write a title to describe the service you provide.</div>
                </div>
                <div className="ten wide column">
                    <div className="field">
                        <textarea rows="2" placeholder="Write a title to describe the service you provide."></textarea>
                    </div>
                    <p>Title must be between 30-80 characters.</p>
                </div>
            </React.Fragment>
        )
    }
}
