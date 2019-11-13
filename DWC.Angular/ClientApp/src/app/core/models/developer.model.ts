export class Developer {
   name: string;
   initials: string;
   image: string;
   summary: string;
   skills: Array<string>;
   webpage: string;
   linkedin: string;
   twitter: string;
   github: string;

   constructor(args: any = {}) {
     this.name = args.name;
     this.initials = args.initials;
     this.image =  this.isValidUrl(args.image) ? args.image : `assets/${args.image}`;
     this.summary = args.summary;
     this.skills = (args.skills) ? args.skills.split(',') : new Array<string>();
     this.webpage = args.webpage;
     this.linkedin = args.linkedin;
     this.twitter = args.twitter;
     this.github = args.github;
   }

   private isValidUrl(value: string): boolean {
    const pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
      '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
      '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
      '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
      '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
      '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator

    return !!pattern.test(value);
  }
}
