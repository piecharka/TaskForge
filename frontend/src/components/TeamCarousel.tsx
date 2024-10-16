import { Link } from "react-router-dom";
import { Team } from "../models/team";
import "../style/TeamCarousel.css"

function TeamCarousel({ teamList }: {teamList: Team[]}) {
  return (
      <div className="carousel">
          {teamList.map(t => {
              return <Link to={'team/' + t.teamId}>
              <div className="project">
                  <p>{t.teamName}</p>
                  <span>{t.users.length} users</span>
              </div>
              </Link>
          }) }
      </div>
  );
}

export default TeamCarousel;