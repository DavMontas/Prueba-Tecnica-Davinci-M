export type ApiBetType = 0 | 1 | 2; // Color=0, ParityOfColor=1, NumberAndColor=2

export interface SpinResultDto {
  number: number;                     // 0..36
  color: 'red' | 'black';             // color SIEMPRE rojo/negro (independiente del número)
  parity: 'even' | 'odd' | 'none';    // 'none' para 0
}

export interface BetSelectionDto {
  color?: 'red' | 'black';
  parity?: 'even' | 'odd';
  number?: number;                    // 0..36
}

export interface CalculatePrizeRequest {
  betType: ApiBetType;
  stake: number;                      // > 0
  selection: BetSelectionDto;
  spin: SpinResultDto;
}

export interface PrizeResult {
  won: boolean;
  payout: number;
  net: number;
}
