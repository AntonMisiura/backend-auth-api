import { UserAuthApi.UIPage } from './app.po';

describe('user-auth-api.ui App', () => {
  let page: UserAuthApi.UIPage;

  beforeEach(() => {
    page = new UserAuthApi.UIPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
