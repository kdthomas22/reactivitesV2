import { Profile } from "./profile";

export interface Activity {
  id: string;
  title: string;
  description: string;
  city: string;
  venue: string;
  category: string;
  hostUsername: string;
  isCancelled: boolean;
  attendees: Profile[];
  isGoing: boolean;
  isHost: boolean;
  host?: Profile;
  date: Date | null;
}

export class Activity implements Activity {
  constructor(init?: ActivityFormValues) {
    Object.assign(this, init);
  }
}

export class ActivityFormValues {
  id?: string = undefined;
  title: string = "";
  category: string = "";
  description: string = "";
  date: Date | null = null;
  city: string = "";
  venue: string = "";

  constructor(activity?: ActivityFormValues) {
    if (activity) {
      this.id = activity.id;
      this.title = activity.title;
      this.category = activity.category;
      this.description = activity.description;
      this.date = activity.date;
      this.venue = activity.venue;
      this.city = activity.city;
    }
  }
}
