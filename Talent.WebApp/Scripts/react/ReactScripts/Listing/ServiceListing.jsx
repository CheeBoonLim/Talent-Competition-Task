import React from 'react';
import { Title } from './Title.jsx';
import { Description } from './Description.jsx';
import { Category } from './Category.jsx';
import { Tags } from './Tags.jsx';
import { ServiceType } from './ServiceType.jsx';
import { AvailableDays } from './AvailableDays.jsx';
import { SkillTrade } from './SkillTrade.jsx';
import { Tokens } from './Tokens.jsx';
import { WorkSamples } from './WorkSamples.jsx';
import { LoggedInBanner } from '../Account/LoggedInBanner.jsx';
import { LoggedInNavigation } from '../Layout/LoggedInNavigation.jsx';

export default class ServiceListing extends React.Component {
    render() {
        return (
            <React.Fragment>
                <LoggedInBanner />
                <LoggedInNavigation />
                <div className="ui container">
                    <div className="listing">
                        <form className="ui form">
                            <div className="ui padded grid">
                                <Title />
                                <Description />
                                <Category />
                                <Tags />
                                <ServiceType />
                                <AvailableDays />
                                <SkillTrade />
                                <Tokens />
                                <WorkSamples />
                                <div className="fourteen wide column">
                                    <div>
                                        <input type="submit" className="ui button right floated" value="Cancel"></input>
                                        <input type="button" className="ui teal button disabled right floated" value="Save"></input>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div >
                </div>
            </React.Fragment>
        )
    }
}
