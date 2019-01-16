/* Tags section */
import React from 'react';

export class Tags extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Tags</h3>
                    <div className="tooltip">Choose tags that fits your services.</div>
                </div>
                <div className="ten wide column">
                    <div className="field">
                        <input name="skill" placeholder="C#, HTML/CSS" type="text"></input>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
