import React from 'react';
import Availability from './Availability.jsx'
import SocialMediaLinkedAccount from './SocialMediaLinkedAccount.jsx';
import Language from './Language.jsx';
import Skill from './Skill.jsx';
import Education from './Education.jsx';
import Certificate from './Certificate.jsx';
import PhotoUpload from './PhotoUpload.jsx';
import SelfIntroduction from './SelfIntroduction.jsx';
import { LoggedInBanner } from '../Account/LoggedInBanner.jsx';
import { LoggedInNavigation } from '../Layout/LoggedInNavigation.jsx';

export default class AccountProfile extends React.Component {
    render() {
        return (
            <div>
                <LoggedInBanner />
                <LoggedInNavigation />
                <section className="page-body">
                    <div className="ui container">
                        <div className="ui container">
                            <div className="profile">
                                <form className="ui form">
                                    <div className="ui grid">
                                        <Availability />
                                        <SocialMediaLinkedAccount />
                                        <Language />
                                        <Skill />
                                        <Education />
                                        <Certificate />
                                        <PhotoUpload />
                                        <SelfIntroduction />
                                    </div>
                                </form>
                            </div >
                        </div>
                    </div>
                </section>
            </div>
        )
    }
}
