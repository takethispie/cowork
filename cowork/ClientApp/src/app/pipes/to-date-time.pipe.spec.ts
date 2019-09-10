import { ToDateTimePipe } from './to-date-time.pipe';

describe('ToDateTimePipe', () => {
  it('create an instance', () => {
    const pipe = new ToDateTimePipe();
    expect(pipe).toBeTruthy();
  });
});
