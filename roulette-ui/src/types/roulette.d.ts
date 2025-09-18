export type ApiBetType = 1 | 2 | 3;

export type BetKind = 'Color' | 'ParityOfColor' | 'NumberAndColor';

export interface SpinResultDto {
  number: number;                    // 0..36
  color: 'red' | 'black';            // independiente del número
  parity: 'zero' | 'even' | 'odd';   // 'zero' para 0 (ojo: antes tenías 'none')
}

export interface BetSelectionDto {
  color?: 'red' | 'black';
  parity?: 'even' | 'odd';
  number?: number;                   // 0..36
}

export interface ResolveBetRequestDto {
  betType: ApiBetType;
  stake: number;                     // > 0
  selection: BetSelectionDto;
}

export interface ResolveBetResponseDto {
  won: boolean;
  prize: number;                     // "monto del premio"
  net: number;                       // conveniencia del backend: prize - stake
  outcome: SpinResultDto;            // resultado del servidor
}
