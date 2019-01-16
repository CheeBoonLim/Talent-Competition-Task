/* SkillTrade section */
import React from 'react';

export class SkillTrade extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Skill Trade</h3>
                    <div className="tooltip">Allow your skills to be traded for another skill</div>
                </div>
                <div className="ten wide column">
                    <div className="form-wrapper">
                        <div className="field">
                            <div className="ui checkbox">
                                <input tabIndex="0" className="hidden" type="checkbox"></input>
                                <label>Allow Skill Trade</label>
                            </div>
                        </div>
                        <div className="field">
                            <input name="desiredSkillExchange" placeholder="List of desired skills" type="text"></input>
                        </div>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
