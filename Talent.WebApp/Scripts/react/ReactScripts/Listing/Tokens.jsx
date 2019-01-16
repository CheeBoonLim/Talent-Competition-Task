/* Tokens section */
import React from 'react';

export class Tokens extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Tokens</h3>
                    <div className="tooltip">Please specify the amount of tokens required for services</div>
                </div>
                <div className="ten wide column">
                    <div className="fields">
                        <div className="five wide field">
                            <label>Tokens per hour</label>
                            <input name="tokensPerHour" placeholder="Tokens per hour" type="text"></input>
                        </div>
                        <div className="six wide field">
                            <label>Total tokens for one-off service</label>
                            <input name="tokensForService" placeholder="Total tokens" type="text" disabled></input>
                        </div>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
